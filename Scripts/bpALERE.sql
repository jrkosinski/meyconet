USE --meycotestsystem




IF OBJECT_ID('dbo.sp_checkuserrole') IS NOT NULL
  DROP PROCEDURE dbo.sp_checkuserrole
  GO



CREATE PROCEDURE [dbo].[sp_checkuserrole]

@userid char(4),
@requiredrole char(4),
@rolemessage char(20) OUT
AS
DECLARE @userrole char(4) = ''
SELECT @userrole = (SELECT top 1 userrole FROM appuser where userid = @userid)
-- Allow all processes for admininstrators
IF @userrole= 'SYAD' OR @userid = 'TRAD'
  BEGIN
    SELECT @rolemessage = 'OK'  
  END
ELSE
  BEGIN
    IF @userrole = @requiredrole
      SELECT @rolemessage = 'OK'  
    ELSE
      SELECT @rolemessage  = 'Not Authorized' 
  END

GO


IF OBJECT_ID('dbo.sp_applogin') IS NOT NULL
  DROP PROCEDURE dbo.sp_applogin
  GO


CREATE PROCEDURE [dbo].[sp_applogin]
@userid char(4),
@passwd char(4),
@loginmessage char(20)OUT
AS  
IF EXISTS	 (SELECT top 1 * FROM appuser where userid = @userid
  AND RTRIM(passwd)  = RTRIM(@passwd)) 
  SELECT @loginmessage = 'OK' 
else
  SELECT @loginmessage = 'Login Failed' 
GO

 


IF OBJECT_ID(N'dbo.GetItemLoctidOnHand', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetItemLoctidOnHand
GO
CREATE FUNCTION dbo.GetItemLoctidOnHand       -- function name
(@item char(15),
 @loctid char(2))
                     -- input parameter name and data type
RETURNS DECIMAL(10,2)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @onhand DECIMAL(16,2)
    SELECT @onhand = ( SELECT SUM(tqty) FROM view_consolidatedinventoryactivity WHERE item = @item AND loctid = @loctid)
 
RETURN @onhand
END;
GO





IF OBJECT_ID(N'dbo.GetItemLoctidAllocation', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetItemLoctidAllocation;
GO
CREATE FUNCTION dbo.GetItemLoctidAllocation       -- function name
(@item char(15),
 @loctid char(2))
                     -- input parameter name and data type
RETURNS DECIMAL(10,2)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @allocated DECIMAL(16,2)
    SELECT @allocated = (SELECT SUM(qtyact) FROM view_coverandlines a
INNER JOIN somast b ON a.sono = b.sono
WHERE a.stocking = 1 AND b.sotype = 'O' AND b.sostat <> 'V' AND b.sostat <> 'C'  AND a.item = @item
AND b.defloc = @loctid)
 
IF @allocated IS NULL 
  BEGIN
    SELECT @allocated = 0  
  END
 
RETURN @allocated
END;
GO
IF OBJECT_ID('dbo.view_coverdata') IS NOT NULL
  DROP VIEW dbo.view_coverdata
  GO
CREATE VIEW view_coverdata
AS
SELECT b.descrip AS material,f.descrip AS itmdesc,f.prcdesc, c.descrip AS spacing, d.descrip AS color, 
e.descrip AS overlap,
 a.* FROM socover a 
LEFT OUTER JOIN  qumaterial b on a.materialid = b.idcol
LEFT OUTER JOIN  quspacing  c on a.spacingid = c.idcol
LEFT OUTER JOIN  qucolor d on a.colorid = d.idcol
LEFT OUTER JOIN  quoverlap e ON a.overlapid = e.idcol
LEFT OUTER JOIN  immaster f ON  a.item = f.item 
GO

IF OBJECT_ID('dbo.view_shipdateprodunits') IS NOT NULL
  DROP VIEW dbo.view_shipdateprodunits
  GO
CREATE VIEW view_shipdateprodunits
AS

SELECT ordate, COUNT(sono) AS socount,  CAST(ROUND(sum(produnits),0) AS int) AS produnits  FROM somast 
WHERE sostat <> 'V'   AND sotype = 'O' AND produnits > 0 GROUP BY ordate  
GO

IF OBJECT_ID('dbo.view_expandedcustalerts') IS NOT NULL
  DROP VIEW dbo.view_expandedcustalerts
  GO
CREATE VIEW view_expandedcustalerts
AS


SELECT a.custno, c.company, b.refdescrip FROM custalerts a
INNER join sysreference b
ON  a.alertid = b.idcol
INNER join arcust c
ON a.custno = c.custno
GO

IF OBJECT_ID('dbo.view_coverandlines') IS NOT NULL
  DROP VIEW dbo.view_coverandlines
  GO
CREATE VIEW view_coverandlines
AS
SELECT a. *,  b.descrip AS itmdesc, b.weight, b.stocking, b.prcdesc
FROM
(select sono, version, cover, item,  'C ' AS source,product, qtyord, qtyord AS qtyact, price, disc, extprice, idcol, prcsqft, sqft, descrip from socover where covertype = 'C '
UNION ALL 
select sono, version, cover, item, source,'LINE' AS product, qtyord, qtyact, price, disc, extprice, idcol, 0 as prcsqft, 0 as sqft, descrip from soline ) A
INNER JOIN immaster b ON a.item = b.item
GO

IF OBJECT_ID('dbo.wsgsp_updatenextsono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_updatenextsono
  GO
CREATE PROCEDURE wsgsp_updatenextsono
@nextsono int
AS
UPDATE appinfo SET nextsono = @nextsono 
GO



IF OBJECT_ID('dbo.wsgsp_getsinglesystemcommentsdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglesystemcommentsdata
  GO
CREATE PROCEDURE wsgsp_getsinglesystemcommentsdata
@idcol int
AS
SELECT * from systemcomments where idcol = @idcol 
GO

IF OBJECT_ID('dbo.wsgsp_deletesoversion') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deletesoversion
  GO
CREATE PROCEDURE wsgsp_deletesoversion
@sono CHAR(10),
@version CHAR(1)
AS
DELETE from soversion WHERE sono = @sono AND version = @version
DELETE from socover WHERE sono = @sono AND version = @version
DELETE from soline WHERE sono = @sono AND version = @version

GO




IF OBJECT_ID('dbo.view_systemcomments') IS NOT NULL
  DROP VIEW dbo.view_systemcomments
  GO
CREATE VIEW view_systemcomments
AS
SELECT * FROM systemcomments 
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getsystemcommentsdata]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getsystemcommentsdata]
  
GO

Create Procedure wsgsp_getsystemcommentsdata
AS	
Begin
 SELECT * FROM view_systemcomments order by code  
End
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getitem]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getitem]
GO

Create Procedure wsgsp_getitem
  @item CHAR(15)
As
Begin
 SELECT * FROM immaster WHERE item = @item
End
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getitembyitem]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getitembyitem]
GO

Create Procedure wsgsp_getitembyitem
  @item CHAR(15)
As
Begin
 SELECT * FROM immaster WHERE item = @item
End
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_geticloctbyloctid]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_geticloctbyloctid]
GO

Create Procedure wsgsp_geticloctbyloctid
  @loctid CHAR(6)
As
Begin
 SELECT * FROM icloct WHERE loctid = @loctid
End
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getallsoreportlinedata]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getallsoreportlinedata]
  
GO

Create Procedure wsgsp_getallsoreportlinedata
	@sono CHAR(10)
As
Begin
 SELECT * FROM view_soreportlinedata WHERE sono = @sono AND (qtyord <> 0 or prcsqft <> 0) ORDER BY sono,  version, cover 
End
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getallsolinedata]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getallsolinedata]
  
GO

IF OBJECT_ID('dbo.view_immasterdata') IS NOT NULL
  DROP VIEW dbo.view_immasterdata
  GO

CREATE VIEW [dbo].[view_immasterdata]
AS
SELECT LEFT(descrip,35) AS shortdescrip, * FROM immaster

GO





IF OBJECT_ID('dbo.view_versiondata') IS NOT NULL
  DROP VIEW dbo.view_versiondata
  GO
CREATE VIEW view_versiondata
AS
SELECT * FROM soversion 
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getsoversions]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getsoversions]
  
GO


Create Procedure wsgsp_getsoversions
	@sono CHAR(10)
As
Begin
 SELECT * FROM view_versiondata WHERE sono = @sono ORDER BY  version
End
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getsoversioncovers]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getsoversioncovers]
  
GO


Create Procedure wsgsp_getsoversioncovers
	@sono CHAR(10),
	@version CHAR(1)
	
As
Begin
 SELECT * FROM view_coverdata WHERE sono = @sono AND version = @version  ORDER BY cover
End
GO



IF OBJECT_ID('dbo.view_versiondata') IS NOT NULL
  DROP VIEW dbo.view_versiondata
  GO
CREATE VIEW view_versiondata
AS
SELECT * FROM soversion 
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getsinglecoverdata]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getsinglecoverdata]
  
GO

Create Procedure wsgsp_getsinglecoverdata
	@sono CHAR(10),
	@version CHAR(1)
As
Begin
	SELECT * FROM view_coverdata WHERE sono = @sono AND version = @version

End
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_getcoverandlines]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_getcoverandlines]
  
GO


Create Procedure wsgsp_getcoverandlines
	@sono CHAR(10),
	@version CHAR(1)
As
Begin
	SELECT * FROM view_coverandlines WHERE sono = @sono AND version = @version order by sono,version, cover, source

End
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_searchsomast]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_searchsomast]
  
GO
 CREATE PROCEDURE [dbo].[wsgsp_searchsomast]	
@sono varchar(10),
@ponum varchar(20),
@custno varchar(6),
@includetype varchar(2),
@lname varchar(20),
@meycono varchar(10),
@begindate datetime,
@enddate datetime

AS	
SELECT @sono = RTRIM(@sono) + '%'
SELECT @ponum = RTRIM(UPPER(@ponum)) + '%'
SELECT @custno = RTRIM(UPPER(@custno)) + '%' 
SELECT @lname = RTRIM(UPPER(@lname)) + '%' 
SELECT @meycono = RTRIM(UPPER(@meycono)) + '%' 

SELECT * from view_somastdata where LTRIM(sono) LIKE  @sono
AND UPPER(ponum) LIKE @ponum  
AND UPPER(custno) LIKE @custno  
AND UPPER(lname) LIKE @lname  
AND UPPER(meycono) LIKE @meycono  
AND CHARINDEX(sotype,RTRIM(@includetype)) > 0
AND ordate BETWEEN @begindate AND @enddate
GO

IF OBJECT_ID('dbo.view_soreportlinedata') IS NOT NULL
  DROP VIEW dbo.view_soreportlinedata
  GO
CREATE VIEW view_soreportlinedata
AS

SELECT v.ordamt,v.tax,v.subtotal,v.adddisc,v.shipping,v.adddiscnote,v.depositreqnote,v.depositreq,v.depositact,v.intcomments,v.custcomments, c.* 
FROM soversion v
INNER JOIN 
(select a.sono,a.version,b.idcol, a.cover,a.product,a.coverstring,a.poolstring, b.prcsqft,b.sqft, a.material,a.straps, a.spacing,a.overlap,a.color,b.qtyord,b.qtyact,b.source, b.price, b.disc, b.extprice, 
b.item, b.itmdesc, b.weight,b.stocking, b.prcdesc, b.descrip 
FROM view_coverdata  a
INNER JOIN view_coverandlines  b
ON a.sono = b.sono AND a.version = b.version AND a.cover = b.cover WHERE a.covertype = 'C ' ) C
ON v.sono = c.sono AND v.version = c.version


GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_deletesocover]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_deletesocover]
  
GO

Create Procedure wsgsp_deletesocover
  @sono char(10),
  @version char(1),
  @cover char(1)
As
Begin
  DELETE FROM soline WHERE sono = @sono AND version = @version  AND cover = @cover
  DELETE FROM socover WHERE sono = @sono AND version = @version  AND cover = @cover
End
GO





IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_deletesoline]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_deletesoline]
  
GO

Create Procedure wsgsp_deletesoline
	@idcol int
As
Begin
	DELETE FROM soline WHERE idcol = @idcol

End
GO





IF OBJECT_ID('dbo.wsgsp_findMiscellaneousSolineData') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findMiscellaneousSolineData
  GO
CREATE PROCEDURE wsgsp_findMiscellaneousSolinedata
@sono CHAR(10),
@version CHAR(1),
@cover CHAR(1)
AS
SELECT * FROM soline WHERE sono = @sono AND version  =  @version
AND cover = @cover  
AND source <> 'C' AND LEFT(source,1) <> 'X' 
GO



IF OBJECT_ID('dbo.wsgsp_getsinglesocoverdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglesocoverdata
  GO
CREATE PROCEDURE wsgsp_getsinglesocoverdata
@sono CHAR(10),
@version CHAR(1),
@cover CHAR(1), 
@covertype CHAR(2)
AS
SELECT * FROM socover WHERE sono = @sono AND version  =  @version 
AND cover = @cover AND covertype = @covertype 
GO



IF OBJECT_ID('dbo.wsgsp_findSolineData') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findSolineData
  GO
CREATE PROCEDURE wsgsp_findSolinedata
@sono CHAR(10),
@version CHAR(1),
@source CHAR(6)
AS
SELECT * FROM soline WHERE sono = @sono AND version  =  @version 
AND source = @source 
GO





IF OBJECT_ID('dbo.wsgsp_findExtPriceScheduleData') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findExtPriceScheduleData
  GO
CREATE PROCEDURE wsgsp_findExtPriceScheduleData
@pschedid int,
@sqft decimal
AS
SELECT TOP 1  * from quprsdetailext  
WHERE pschedid = @pschedid  and sqft >= @sqft
ORDER BY sqft 
GO


IF OBJECT_ID('dbo.wsgsp_findPriceScheduleData') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findPriceScheduleData
  GO
CREATE PROCEDURE wsgsp_findPriceScheduleData
@pschedid int,
@sqft decimal
AS
SELECT TOP 1  * from quprsdetail  
WHERE pschedid = @pschedid  and sqft >= @sqft
ORDER BY sqft 
GO

IF OBJECT_ID('dbo.wsgsp_findPriceLocatorData') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findPriceLocatorData
  GO
CREATE PROCEDURE wsgsp_findPriceLocatorData
@item char(15),
@spacingid int
AS
SELECT * FROM quprslocator WHERE item = @item AND spacingid = @spacingid
GO





IF OBJECT_ID('dbo.wsgsp_getqucolordata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getqucolordata
  GO
CREATE PROCEDURE wsgsp_getqucolordata
AS
SELECT * from view_qucolordata ORDER BY descrip 
GO
IF OBJECT_ID('dbo.view_qucolordata') IS NOT NULL
  DROP VIEW dbo.view_qucolordata
  GO
CREATE VIEW view_qucolordata
AS
SELECT * FROM qucolor 
GO
IF OBJECT_ID('dbo.wsgsp_getquoverlapdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getquoverlapdata
  GO
CREATE PROCEDURE wsgsp_getquoverlapdata
AS
SELECT * from view_quoverlapdata ORDER BY descrip 
GO
IF OBJECT_ID('dbo.view_quoverlapdata') IS NOT NULL
  DROP VIEW dbo.view_quoverlapdata
  GO
CREATE VIEW view_quoverlapdata
AS
SELECT * FROM quoverlap 
GO

IF OBJECT_ID('dbo.wsgsp_getqumaterialdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getqumaterialdata
  GO
CREATE PROCEDURE wsgsp_getqumaterialdata
AS
SELECT * from view_qumaterialdata ORDER BY descrip 
GO
IF OBJECT_ID('dbo.view_qumaterialdata') IS NOT NULL
  DROP VIEW dbo.view_qumaterialdata
  GO
CREATE VIEW view_qumaterialdata
AS
SELECT * FROM qumaterial 
GO



IF OBJECT_ID('dbo.wsgsp_getquoteitem') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getquoteitem
  GO
CREATE PROCEDURE wsgsp_getquoteitem
@code CHAR(10)
AS
SELECT LEFT(descrip,35) AS shortdescrip, * from view_immasterdata WHERE LEFT(misccode,2) = @code  ORDER BY item 
GO




IF OBJECT_ID('dbo.view_somastdata') IS NOT NULL
  DROP VIEW dbo.view_somastdata
  GO
CREATE VIEW view_somastdata
AS
SELECT a.*,b.company FROM somast a
INNER join arcust b ON a.custno = b.custno

GO

IF OBJECT_ID('dbo.wsgsp_getsomastdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsomastdata
  GO
CREATE PROCEDURE wsgsp_getsomastdata
AS

SELECT * FROM view_somastdata order by sono
GO


IF OBJECT_ID('dbo.wsgsp_getsinglesomastdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglesomastdata
  GO
CREATE PROCEDURE wsgsp_getsinglesomastdata
@idcol int
AS
SELECT * FROM sommast where idcol = @idcol
GO
IF OBJECT_ID('dbo.wsgsp_getsinglesoaddrdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglesoaddrdata
  GO
CREATE PROCEDURE wsgsp_getsinglesoaddrdata
@somastid int
AS
SELECT * FROM soaddr where somastid = @somastid
GO



IF OBJECT_ID('dbo.wsgsp_getdefaultshipto') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getdefaultshipto
  GO
CREATE PROCEDURE wsgsp_getdefaultshipto
@custid int

AS

SELECT * FROM aracadr WHERE defaship = 'Y' and custid  = @custid
GO

IF OBJECT_ID('dbo.wsgsp_getsomastbysono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsomastbysono
  GO
CREATE PROCEDURE wsgsp_getsomastbysono
@sono char(10)
AS
SELECT * from somast where sono = @sono 
GO


IF OBJECT_ID('dbo.wsgsp_getappinfo') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getappinfo
  GO
CREATE PROCEDURE dbo.wsgsp_getappinfo
AS
SELECT * from appinfo
GO






IF OBJECT_ID('dbo.wsgsp_getsinglequprsdetaildata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglequprsdetaildata
  GO
CREATE PROCEDURE wsgsp_getsinglequprsdetaildata
@idcol int
AS
SELECT * from quprsdetail where idcol = @idcol 
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_Deletequprslocator]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_Deletequprslocator]
  
GO

Create Procedure wsgsp_Deletequprslocator
	@idcol int
	
As
Begin
   DELETE FROM quprslocator
Where		
		[idcol] = @idcol
End
GO





IF OBJECT_ID('dbo.view_quprsdetaildata') IS NOT NULL
  DROP VIEW dbo.view_quprsdetaildata
  GO
CREATE VIEW view_quprsdetaildata
AS
SELECT * FROM quprsdetail
GO

IF OBJECT_ID('dbo.wsgsp_getquprsdetaildata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getquprsdetaildata
  GO
CREATE PROCEDURE wsgsp_getquprsdetaildata
@pschedid int
AS
SELECT * from view_quprsdetaildata WHERE pschedid = @pschedid ORDER BY sqft
GO




IF OBJECT_ID('dbo.view_quprsheaddata') IS NOT NULL
  DROP VIEW dbo.view_quprsheaddata
  GO
CREATE VIEW view_quprsheaddata
AS
SELECT * FROM quprshead
GO

IF OBJECT_ID('dbo.wsgsp_getquprsheaddata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getquprsheaddata
  GO
CREATE PROCEDURE wsgsp_getquprsheaddata
AS
SELECT * from view_quprsheaddata ORDER BY descrip 
GO

IF OBJECT_ID('dbo.wsgsp_getsinglequprsheaddata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglequprsheaddata
  GO
CREATE PROCEDURE wsgsp_getsinglequprsheaddata
@idcol int
AS
SELECT * from quprshead where idcol = @idcol 
GO




IF OBJECT_ID('dbo.wsgsp_getimmasterdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getimmasterdata
  GO
CREATE PROCEDURE wsgsp_getimmasterdata
AS
SELECT * from view_immasterdata ORDER BY item 
GO

IF OBJECT_ID('dbo.wsgsp_getsingleimmasterdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingleimmasterdata
  GO
CREATE PROCEDURE wsgsp_getsingleimmasterdata
@item char(25)
AS
SELECT * from immaster  where item  = @item 
GO



IF OBJECT_ID('dbo.view_immasterdata') IS NOT NULL
  DROP VIEW dbo.view_immasterdata
  GO
CREATE VIEW view_immasterdata
AS
SELECT LEFT(descrip,35) AS shortdescrip, * FROM immaster
GO

IF OBJECT_ID('dbo.wsgsp_updatequspacingdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_updatequspacingdata
  GO
CREATE PROCEDURE wsgsp_updatequspacingdata
@idcol int,
@descrip CHAR(30),
@strpmult DECIMAL(5,3),
@userid char (4)
AS 
UPDATE  quspacing SET descrip = @descrip, strpmult = @strpmult, lckstat = ' ', 
lckdate = GETDATE() , lckuser = @userid WHERE idcol = @idcol

 
GO




IF OBJECT_ID('dbo.wsgsp_getsinglequspacingdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglequspacingdata
  GO
CREATE PROCEDURE wsgsp_getsinglequspacingdata
@idcol int
AS
SELECT * from quspacing where idcol = @idcol 
GO


IF OBJECT_ID('dbo.wsgsp_getquspacingdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getquspacingdata
  GO
CREATE PROCEDURE wsgsp_getquspacingdata
AS
SELECT * from view_quspacingdata ORDER BY descrip 
GO

IF OBJECT_ID('dbo.view_quspacingdata') IS NOT NULL
  DROP VIEW dbo.view_quspacingdata
  GO
CREATE VIEW view_quspacingdata
AS

SELECT descrip, idcol,code,strpmult from quspacing
GO


IF OBJECT_ID('dbo.wsgsp_getsinglequprslocatordata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglequprslocatordata
  GO
CREATE PROCEDURE wsgsp_getsinglequprslocatordata
@idcol int
AS
SELECT * from quprslocator WHERE idcol = @idcol
GO



IF OBJECT_ID('dbo.wsgsp_getprslocatordata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getprslocatordata
  GO
CREATE PROCEDURE wsgsp_getprslocatordata
AS
SELECT * from view_prslocatordata ORDER BY item,spacing 
GO

IF OBJECT_ID('dbo.view_prslocatordata') IS NOT NULL
  DROP VIEW dbo.view_prslocatordata
  GO
CREATE VIEW view_prslocatordata
AS

SELECT c.item,d.descrip AS spacing, pschedid, prcfact, b.descrip AS psdescrip, a.idcol from quprslocator a
INNER JOIN quprshead b ON a.pschedid = b.idcol 
INNER JOIN immaster c ON a.item = c.item 
INNER JOIN quspacing d ON a.spacingid = d.idcol 
GO


IF OBJECT_ID('dbo.view_customershiptolist') IS NOT NULL
  DROP VIEW dbo.view_customershiptolist
GO
-- transactionid - Identity field
CREATE VIEW view_customershiptolist
AS
SELECT * from aracadr
GO


IF OBJECT_ID('dbo.wsgsp_getprsdetail') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getprsdetail
  GO
CREATE PROCEDURE wsgsp_getprsdetail
@pschedid int
AS
SELECT * from quprsdetail where pschedid = @pschedid order by sqft 
GO

IF OBJECT_ID('dbo.wsgsp_updateshiptocustid') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_updateshiptocustid
  GO
CREATE PROCEDURE wsgsp_updateshiptocustid
AS
UPDATE aracadr SET custid = arcust.idcol  FROM arcust 
WHERE aracadr.custno = arcust.custno
GO
 

IF OBJECT_ID('dbo.wsgsp_checkcshipno') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_checkcshipno
  GO
CREATE PROCEDURE wsgsp_checkcshipno

@cshipno char(6),
@custid int,
@cshipnomessage nvarchar(40) OUT
AS
IF NOT EXISTS (SELECT cshipno FROM aracadr where custid = @custid 
  AND cshipno =  @cshipno)
  BEGIN
    SELECT @cshipnomessage = 'OK'  
  END
ELSE
  BEGIN
     SELECT @cshipnomessage  = 'Ship To Number ' +   @cshipno + ' already exists.' 
  END
GO





IF OBJECT_ID('dbo.wsgsp_locktable') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_locktable
  GO
CREATE PROCEDURE wsgsp_locktable
@idcol int,
@tablename CHAR(40),
@userid CHAR(4),
@returnmessage CHAR(50) OUTPUT
AS
DECLARE @sql NVARCHAR(MAX)
DECLARE @sqlparameterlist NVARCHAR(MAX)
DECLARE @ParameterList NVARCHAR(1000)
DECLARE @charid CHAR(7)
DECLARE @okmessage CHAR(2)
DECLARE @lckuser CHAR(4)
DECLARE @spaces CHAR(2)
DECLARE @nfindmessage CHAR(20)
DECLARE @lckmessage CHAR(20)
SET @okmessage = 'OK'
SET @spaces = '  '
SET @charid = CAST( @idcol AS varchar)
SET @nfindmessage = 'Record not found'
SET @lckmessage = 'Record is locked by '
 
SET @sqlparameterlist = '@returnmessage CHAR(45) OUTPUT' 
SET @sql = '
DECLARE @lckuser CHAR(4) = ' + QUOTENAME(@userid,'''') +
'IF NOT EXISTS (SELECT * FROM ' + @tablename + ' WHERE idcol = ' +  @charid + ')
  SELECT @returnmessage  = ' + QUOTENAME(@nfindmessage,'''') +
'ELSE
  IF EXISTS (SELECT * FROM ' + @tablename + ' WHERE idcol = '  + @charid + 
  ' AND lckstat = ' + QUOTENAME(@spaces,'''') + ')
    BEGIN
      SELECT @returnmessage = ' + QUOTENAME(@okmessage,'''') + '
      UPDATE ' + @tablename + ' SET lckstat = '''+ 'L' + ''' , lckdate = GETDATE(), lckuser = ' + QUOTENAME(@userid,'''')  + '  where idcol  = ' + @charid+ '
    END  
  ELSE 
   BEGIN  
    
     SELECT @lckuser = (SELECT lckuser FROM ' + @tablename + ' where idcol = ' +  @charid + ') 
     SET @returnmessage = ' + QUOTENAME(@lckmessage,'''') + '
     SELECT @returnmessage = RTRIM(@returnmessage)  + SPACE(2)+    @lckuser 
     
   END'  

EXEC sp_executesql @sql,@sqlparameterlist,@returnmessage OUTPUT
SELECT @returnmessage 

go



IF OBJECT_ID('dbo.wsgsp_getsingletablerow') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingletablerow
  GO
CREATE PROCEDURE wsgsp_getsingletablerow
@idcol int,
@tablename char(20)

AS 
DECLARE @stringid CHAR(10)
SET @stringid = CAST( @idcol AS varchar)
DECLARE @SQL nvarchar(max)
SELECT @SQL = 'SELECT * FROM ' + @tablename + ' WHERE idcol = ' + @stringid
EXEC sp_executeSQL @SQL   
GO



IF OBJECT_ID('dbo.wsgsp_deletetablerow') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deletetablerow
  GO
CREATE PROCEDURE wsgsp_deletetablerow
@idcol int,
@tablename char(20)

AS 
DECLARE @stringid CHAR(10)
SET @stringid = CAST( @idcol AS varchar)
DECLARE @SQL nvarchar(max)
SELECT @SQL = 'DELETE FROM ' + @tablename + ' WHERE idcol = ' + @stringid
EXEC sp_executeSQL @SQL   
GO



IF OBJECT_ID('dbo.wsgsp_unlocktable') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_unlocktable
  GO
CREATE PROCEDURE wsgsp_unlocktable
@idcol int,
@tablename char(40)

AS 
DECLARE @stringid CHAR(10)
SET @stringid = CAST( @idcol AS varchar)
DECLARE @SQL nvarchar(max)
SELECT @SQL = 'UPDATE ' + @tablename + ' SET lckstat = '''' WHERE idcol = ' + @stringid
EXEC sp_executeSQL @SQL   
GO




IF OBJECT_ID('dbo.wsgsp_lockarcust') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_lockarcust
  GO
CREATE PROCEDURE wsgsp_lockarcust
@idcol int,
@userid CHAR(4),
@lckuser CHAR(4), 
@returnmessage CHAR(30) OUT 
AS 
if NOT exists (select * from arcust where idcol = @idcol)
  SET @returnmessage = 'Customer Not Found'
ELSE
  if exists (select * from arcust where idcol = @idcol AND lckstat =  ' ')
    BEGIN
      SET @returnmessage = 'OK'   
      UPDATE arcust SET lckstat = 'L', lckuser = @userid  where idcol = @idcol
   END
  ELSE 
    BEGIN
      SELECT @lckuser = arcust.lckuser FROM arcust WHERE arcust.idcol = @idcol
      SET @returnmessage =  'Record has been locked by ' + @lckuser 
    END  
 SELECT @returnmessage
go


IF OBJECT_ID('dbo.wsgsp_getcustomerdatabycustno') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getcustomerdatabycustno
  GO
CREATE PROCEDURE wsgsp_getcustomerdatabycustno
@custno char(6)
AS 
select * from arcust where RTRIM(custno) = RTRIM(@custno) 
GO






IF OBJECT_ID('dbo.wsgsp_getcustomershiptodata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getcustomershiptodata
  GO
CREATE PROCEDURE wsgsp_getcustomershiptodata
@custid int
AS 
select * from aracadr where custid = @custid ORDER BY cshipno  
GO

IF OBJECT_ID('dbo.wsgsp_getsingleshiptodata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingleshiptodata
  GO
CREATE PROCEDURE wsgsp_getsingleshiptodata
@idcol int
AS 
select * from aracadr where idcol = @idcol  
GO



IF OBJECT_ID('dbo.wsgsp_getcustomerdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getcustomerdata
  GO
CREATE PROCEDURE wsgsp_getcustomerdata
@idcol int
AS 
select * from arcust where idcol = @idcol  
GO


IF OBJECT_ID('dbo.wsgsp_checkcustno') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_checkcustno
  GO
CREATE PROCEDURE wsgsp_checkcustno

@custno char(6),
@custmessage nvarchar(40) OUT
AS
IF NOT EXISTS (SELECT custno FROM arcust where custno = @custno)
  BEGIN
    SELECT @custmessage = 'OK'  
  END
ELSE
  BEGIN
     SELECT @custmessage  = 'Customer ' +   @custno + ' already exists.' 
  END
GO

IF OBJECT_ID('dbo.wsgsp_getinspmastbysono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getinspmastbysono
  GO
CREATE PROCEDURE wsgsp_getinspmastbysono
@sono char(10)
AS
SELECT * from inspmast where sono = @sono 
GO
IF OBJECT_ID('dbo.wsgsp_getsysrefgroup') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsysrefgroup
  GO
CREATE PROCEDURE wsgsp_getsysrefgroup
AS
SELECT * from sysrefgroup order by groupname 
GO

IF OBJECT_ID('dbo.view_sysreference') IS NOT NULL
  DROP VIEW dbo.view_sysreference
  GO
CREATE VIEW view_sysreference
AS
SELECT * FROM sysreference

GO

IF OBJECT_ID('dbo.wsgsp_getinsplines') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getinsplines
  GO
CREATE PROCEDURE wsgsp_getinsplines

	@sono CHAR(10),
	@version CHAR(1)
As
Begin
 SELECT * FROM view_insplinedata WHERE sono = @sono AND version = @version  ORDER BY descrip
End
GO


IF OBJECT_ID('dbo.wsgsp_getinspversions') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getinspversions
  GO
CREATE PROCEDURE wsgsp_getinspversions

	@sono CHAR(10)
As
Begin
 SELECT * FROM view_inspversiondata WHERE sono = @sono ORDER BY  version
End
GO

IF OBJECT_ID('dbo.view_inspversions') IS NOT NULL
  DROP VIEW dbo.view_inspversions
  GO
CREATE VIEW view_inspversions
AS
SELECT  sono, version, lckuser, lckdate, adduser, adddate, idcol, lckstat
FROM inspversion
GO

IF OBJECT_ID('dbo.view_inspversiondata') IS NOT NULL
  DROP VIEW dbo.view_inspversiondata
  GO
CREATE VIEW view_inspversiondata
AS
SELECT * FROM inspversion

GO
IF OBJECT_ID('dbo.view_insplinedata') IS NOT NULL
  DROP VIEW dbo.view_insplinedata
  GO
CREATE VIEW view_insplinedata
AS
SELECT * FROM inspline

GO
IF OBJECT_ID('dbo.view_expandedsysreference') IS NOT NULL
  DROP VIEW dbo.view_expandedsysreference
  GO
CREATE VIEW view_expandedsysreference
AS

select b.groupname, b.idcol AS groupidcol , a. * from sysreference a
inner join sysrefgroup b on a.groupid = b.idcol
GO


IF OBJECT_ID('dbo.view_sysrefwithname') IS NOT NULL
  DROP VIEW dbo.view_sysrefwithname
  GO
CREATE VIEW view_sysrefwithname
AS

select b.groupname, a. * from sysreference a
inner join sysrefgroup b on a.groupid = b.idcol

GO

IF OBJECT_ID('dbo.wsgsp_getsysreferences') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsysreferences
 
GO

Create Procedure wsgsp_getsysreferences
@groupid int
As
SELECT * FROM view_sysreference WHERE groupid = @groupid ORDER by refdescrip
GO

IF OBJECT_ID('dbo.wsgsp_getsysreferencesbyname') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsysreferencesbyname
 
GO

Create Procedure wsgsp_getsysreferencesbyname
@groupname char(25)
AS
SELECT * FROM view_sysreference WHERE groupid IN (SELECT IDCOL FROM sysrefgroup WHERE groupname = @groupname) ORDER BY refdescrip
GO

IF OBJECT_ID('dbo.wsgsp_getsinglesysreference') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsinglesysreference
GO

Create Procedure wsgsp_getsinglesysreference
	@idcol int
	As
Begin
	SELECT * FROM sysreference WHERE idcol = @idcol

End
GO

IF OBJECT_ID('dbo.wsgsp_deleteinspline') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deleteinspline
GO

Create Procedure wsgsp_deleteinspline
	@idcol int
As
Begin
	DELETE FROM inspline WHERE idcol = @idcol
End
GO
IF OBJECT_ID('dbo.wsgsp_findMiscellaneousInsplineData') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findMiscellaneousInsplineData
  GO
CREATE PROCEDURE wsgsp_findMiscellaneousInsplinedata
@sono CHAR(10),
@version CHAR(1)
AS
SELECT * FROM inspline WHERE sono = @sono AND version  =  @version

GO

IF OBJECT_ID(N'dbo.GetContactName', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetContactName;
GO
CREATE FUNCTION dbo.GetContactName           -- function name
(@idcol int)                     -- input parameter name and data type
RETURNS CHAR(100)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @contactname CHAR(100)
IF EXISTS (select contactname from contact sysreference where idcol = @idcol
 )
 
   SELECT @contactname = ( select contactname from contact where idcol = @idcol)
ELSE
   SELECT @contactname = 'Not Found'
RETURN @contactname
END;
GO

IF OBJECT_ID(N'dbo.GetRefDescription', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetRefDescription;
GO
CREATE FUNCTION dbo.GetRefDescription           -- function name
(@refcode int)                     -- input parameter name and data type
RETURNS CHAR(100)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @refdescrip CHAR(100)
IF EXISTS (select refdescrip from sysreference where idcol = @refcode
 )
 
   SELECT @refdescrip = ( select LEFT(refdescrip,100) from sysreference where idcol = @refcode)
ELSE
   SELECT @refdescrip = 'Not Found'
RETURN @refdescrip
END;
GO

IF OBJECT_ID(N'dbo.GetMaterialDescription', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetMaterialDescription;
GO
CREATE FUNCTION dbo.GetMaterialDescription           -- function name
(@refcode int)                     -- input parameter name and data type
RETURNS CHAR(100)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @refdescrip CHAR(100)
IF EXISTS (select descrip from qumaterial where idcol = @refcode
 )
 
   SELECT @refdescrip = ( select LEFT(descrip,100) from qumaterial where idcol = @refcode)
ELSE
   SELECT @refdescrip = 'Not Found'
RETURN @refdescrip
END;
GO
IF OBJECT_ID(N'dbo.GetColorDescription', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetColorDescription;
GO
CREATE FUNCTION dbo.GetColorDescription           -- function name
(@refcode int)                     -- input parameter name and data type
RETURNS CHAR(100)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @refdescrip CHAR(100)
IF EXISTS (select descrip from qucolor where idcol = @refcode
 )
 
   SELECT @refdescrip = ( select LEFT(descrip,100) from qucolor where idcol = @refcode)
ELSE
   SELECT @refdescrip = 'Not Found'
RETURN @refdescrip
END;
GO
IF OBJECT_ID(N'dbo.GetSpacingDescription', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetSpacingDescription;
GO

CREATE FUNCTION dbo.GetSpacingDescription           -- function name
(@refcode int)                     -- input parameter name and data type
RETURNS CHAR(100)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @refdescrip CHAR(100)
IF EXISTS (select descrip from quspacing where idcol = @refcode
 )
 
   SELECT @refdescrip = ( select LEFT(descrip,100) from quspacing where idcol = @refcode)
ELSE
   SELECT @refdescrip = 'Not Found'
RETURN @refdescrip
END;
GO

IF OBJECT_ID('dbo.view_inspmastexpanded') IS NOT NULL
  DROP VIEW dbo.view_inspmastexpanded
  GO
CREATE VIEW view_inspmastexpanded
AS
SELECT b.intcomments,a.*, dbo.getrefdescription(matcond) as matcondition,
dbo.getrefdescription(webcond) as webcondition,
dbo.getrefdescription(threadcond) as threadcondition,
dbo.getrefdescription(inbcarrierid) as carriername,
dbo.getrefdescription(manufacturer) AS manuname,
dbo.getrefdescription(locationid) AS locationname,
dbo.getrefdescription(springcond) as springscondition,
dbo.getmaterialdescription(materialid) as material,
dbo.getcolordescription(colorid) as color,
dbo.getspacingdescription(spacingid) as spacing
FROM inspmast a
INNER JOIN 
(SELECT * FROM
(SELECT sono,version, intcomments,
row_number() over (partition by sono  order by sono,version  )  as RowNum 
from soversion)  x
WHERE x.RowNum = 1) b
ON a.sono = b.sono
 

GO
IF OBJECT_ID('dbo.view_inspreport') IS NOT NULL
  DROP VIEW dbo.view_inspreport
  GO
CREATE VIEW view_inspreport
AS
SELECT a.*,b.qtyord, b.descrip AS linedescrip,b.version, b.source FROM view_inspmastexpanded a
LEFT JOIN inspline b
ON b.sono = a.sono
GO

IF OBJECT_ID('dbo.wsgsp_getinspreportdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getinspreportdata
  GO
CREATE PROCEDURE wsgsp_getinspreportdata
@sono char(10)
AS
SELECT * from view_inspreport where sono = @sono order by version, source 
GO


IF OBJECT_ID('dbo.wsgsp_deleteallinspversiondata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deleteallinspversiondata
  GO
CREATE PROCEDURE wsgsp_deleteallinspversiondata
@sono char(10),
@version char(1)
AS
DELETE FROM inspversion WHERE sono = @sono AND version = @version
DELETE FROM inspline WHERE sono = @sono AND version = @version

GO

IF OBJECT_ID('dbo.wsgsp_updatenextplannumber') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_updatenextplannumber
  GO
CREATE PROCEDURE dbo.wsgsp_updatenextplannumber
@nextplannumber int
AS
UPDATE appinfo SET nextplannumber = @nextplannumber
GO
IF OBJECT_ID('dbo.wsgsp_unlocktables') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_unlocktables
  GO
CREATE PROCEDURE dbo.wsgsp_unlocktables
AS
UPDATE somast SET lckstat = ' '
UPDATE arcust SET lckstat = ' '
GO

IF OBJECT_ID('dbo.wsgsp_findSolineDatabySono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findSolineDatabySono
  GO
CREATE PROCEDURE wsgsp_findSolineDatabySono
@sono CHAR(10)
AS
SELECT * FROM soline WHERE sono = @sono   
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_trackingsearchsomast]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_trackingsearchsomast]
  
GO
 CREATE PROCEDURE [dbo].[wsgsp_trackingsearchsomast]	
@sono varchar(10),
@ponum varchar(20),
@invno varchar(10),
@custno varchar(6),
@lname varchar(20),
@meycono varchar(10),
@begindate datetime,
@enddate datetime

AS	
SELECT @sono = RTRIM(@sono) + '%'
SELECT @invno =  RTRIM(UPPER(@invno)) + '%'
SELECT @ponum = RTRIM(UPPER(@ponum)) + '%'
SELECT @custno = RTRIM(UPPER(@custno)) + '%' 
SELECT @lname = RTRIM(UPPER(@lname)) + '%' 
SELECT @meycono = RTRIM(UPPER(@meycono)) + '%' 

SELECT *, '  /  /    ' AS procdate,SPACE(5) as trakcode, 'N' AS hascomment from view_somastdata where LTRIM(sono) LIKE  @sono
AND UPPER(ponum) LIKE @ponum  
AND LTRIM(invno) LIKE @invno  
AND UPPER(custno) LIKE @custno  
AND UPPER(lname) LIKE @lname  
AND UPPER(meycono) LIKE @meycono  
AND sodate BETWEEN @begindate AND @enddate
AND sostat <> 'V'
ORDER BY sono
GO

IF OBJECT_ID('dbo.wsgsp_getsomastdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsomastdata
  GO
CREATE PROCEDURE wsgsp_getsomastdata
AS

SELECT * FROM view_somastdata order by sono
GO


IF OBJECT_ID('dbo.wsgsp_getview_somastdatabysono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getview_somastdatabysono
  GO
CREATE PROCEDURE wsgsp_getview_somastdatabysono
@sono CHAR(10)
AS
SELECT * FROM view_somastdata WHERE sono = @sono
GO


IF OBJECT_ID('dbo.view_customeralertdata') IS NOT NULL
  DROP VIEW dbo.view_customeralertdata
  GO
CREATE VIEW view_customeralertdata
AS
SELECT a.*,b.company, b.email as soaddremail, c.email, c.emailquote as emailquote,c.emailorder as emailorder , c.emailinvoice as emailinvoice,
c.faxquote as faxquote, c.faxorder as faxorder, c.faxinvoice as faxinvoice,
 c.source, c.faxno FROM somast a
INNER join soaddr b ON a.sono = b.sono
INNER join arcust c on a.custno = c.custno
GO


IF OBJECT_ID('dbo.wsgsp_getcustomeralertdatabysono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getcustomeralertdatabysono
  GO
CREATE PROCEDURE wsgsp_getcustomeralertdatabysono
@sono char(10)
AS
SELECT * from view_customeralertdata where sono = @sono 
GO

IF OBJECT_ID('dbo.wsgsp_getorderitem') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getorderitem
  GO
CREATE PROCEDURE wsgsp_getorderitem
@misccode CHAR(10)
AS
SELECT * from view_immasterdata WHERE misccode = @misccode  ORDER BY 1 
GO

IF OBJECT_ID('dbo.wsgsp_getpdfsodata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getpdfsodata
  GO
CREATE PROCEDURE wsgsp_getpdfsodata
@lckinterval INT
AS
SELECT * FROM somast where lckdate >= (GETDATE() - @lckinterval)  AND sostat <> 'V'
ORDER BY sono
GO
IF OBJECT_ID('dbo.wsgsp_clearsomastlocks') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_clearsomastlocks
  GO
CREATE PROCEDURE wsgsp_clearsomastlocks
AS
UPDATE somast SET lckstat = ' '
GO
IF OBJECT_ID('dbo.wsgsp_cleararcustlocks') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_cleararcustlocks
  GO
CREATE PROCEDURE wsgsp_cleararcustlocks
AS
UPDATE arcust SET lckstat = ' '
GO
IF OBJECT_ID('dbo.wsgsp_clearemailaddress') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_clearemailaddress
  GO
CREATE PROCEDURE wsgsp_clearemailaddress
AS
UPDATE emailaddress SET lckstat = ' '
GO
IF OBJECT_ID('dbo.wsgsp_clearproductionunitschedule') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_clearproductionunitschedule
  GO
CREATE PROCEDURE wsgsp_clearproductionunitschedule
AS
UPDATE productionunitschedule SET lckstat = ' '
GO
IF OBJECT_ID('dbo.wsgsp_clearcapacitycalendar') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_clearcapacitycalendar
  GO
CREATE PROCEDURE wsgsp_clearcapacitycalendar
AS
UPDATE capacitycalendar SET lckstat = ' '
GO

IF OBJECT_ID('dbo.wsgsp_cleararacadrlocks') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_cleararacadrlocks
  GO
CREATE PROCEDURE wsgsp_cleararacadrlocks
AS
UPDATE aracadr SET lckstat = ' '
GO
IF OBJECT_ID('dbo.wsgsp_clearwarrantylocks') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_clearwarrantylocks
  GO
CREATE PROCEDURE wsgsp_clearwarrantylocks
AS
UPDATE warranty SET lckstat = ' ' WHERE lckstat <> ' '
GO


IF OBJECT_ID('dbo.wsgsp_getsoproduct') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsoproduct
 GO 
CREATE PROCEDURE wsgsp_getsoproduct
@sono CHAR(10)
AS
SELECT TOP 1 * FROM socover WHERE sono = @sono ORDER BY version,cover
GO

IF OBJECT_ID('dbo.wsgsp_getsomastbycustnoponum') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsomastbycustnoponum
  GO
CREATE PROCEDURE wsgsp_getsomastbycustnoponum
@custno char(6),
@ponum varchar(40), 
@oldplan char(10)
AS
SELECT * from somast where sodate > (GETDATE() - 365) AND ( sostat <> 'V' AND custno = @custno  AND UPPER(ponum)  LIKE  '%' + UPPER(@ponum) + '%' )
OR  ( RTRIM(@oldplan) <> SPACE(0) AND oldplan = @oldplan) 
GO

IF OBJECT_ID('dbo.view_salesanalysislines') IS NOT NULL
  DROP VIEW dbo.view_salesanalysislines
  GO
CREATE VIEW view_salesanalysislines
AS
SELECT a.*, b.custno, b.sodate, 000000.00 as paidamt, CAST ( '1990-01-01' AS datetime) AS datepaid ,  b.ordamt, b.tax, b.shpamt, b.ponum,b.salesmn,b.sostat,
b.sotype, b.invno, b.invdte, b.ordate, b.meycono, b.oldplan, b.enterqu, b.quconvert,
c.company, c.source AS custsource
 FROM view_coverandlines a 
INNER JOIN  somast b
ON a.sono = b.sono
INNER JOIN arcust c 
ON b.custno = c.custno
GO

IF OBJECT_ID('dbo.wsgsp_getviewsalesanalysislines') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getviewsalesanalysislines
  GO
CREATE PROCEDURE wsgsp_getviewsalesanalysislines
@startdate datetime,
@enddate datetime

AS
SELECT * FROM view_salesanalysislines WHERE sodate >= @startdate AND 
sodate <= @enddate
GO

IF OBJECT_ID('dbo.wsgsp_lockinvoicing') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_lockinvoicing
  GO
CREATE PROCEDURE wsgsp_lockinvoicing
@userid CHAR(4),
@returnmessage CHAR(50) OUT 

AS 

  DECLARE @invoiceuser char(4)
  SELECT @invoiceuser = (SELECT invoiceuser FROM appinfo)
  if  @invoiceuser = SPACE(4)
    BEGIN
      SET @returnmessage = 'OK'   
      UPDATE appinfo SET invoiceuser = @userid
   END
  ELSE 
    BEGIN
      SET @returnmessage =  'Invoicing is being peformed by ' + @invoiceuser
    END  
 SELECT @returnmessage
go

IF OBJECT_ID('dbo.wsgsp_unlockinvoicing') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_unlockinvoicing
  GO
CREATE PROCEDURE wsgsp_unlockinvoicing
AS 
UPDATE appinfo SET invoiceuser = ' '
GO
IF OBJECT_ID('dbo.view_solasttrackingdata') IS NOT NULL
  DROP VIEW dbo.view_solasttrackingdata
  GO
CREATE VIEW view_solasttrackingdata
AS
select sono, b.code,sentmail, b.descrip,b.oktoinvoice, b.mustbeinvoiced, b.ondescrip, b.message,b.sendso,  b.sendftp, b.filetosend, b.alertinterval, b.alertmessage, b.internalmail, X.idcol, X.stepid, upstrack, trackdate,seq 
FROM (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol WHERE RowNum = 1
GO
IF OBJECT_ID('dbo.wsgsp_getsolasttrackingdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsolasttrackingdata
  GO
CREATE PROCEDURE wsgsp_getsolasttrackingdata
@sono CHAR(10)
AS 
select sono, b.code,sentmail, b.descrip,b.oktoinvoice, b.mustbeinvoiced, b.ondescrip, b.message,b.sendso,  b.sendftp, b.filetosend, b.alertinterval, b.alertmessage, b.internalmail, X.idcol, X.stepid, upstrack, trackdate,seq 
FROM (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE sono = @sono and cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol WHERE RowNum = 1
 
GO 

IF OBJECT_ID('dbo.wsgsp_gettrackingcodedata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_gettrackingcodedata
GO
CREATE PROCEDURE wsgsp_gettrackingcodedata

AS

SELECT * FROM step
ORDER BY code
GO

IF OBJECT_ID('dbo.wsgsp_getsingletrackingstep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingletrackingstep
GO
CREATE PROCEDURE wsgsp_getsingletrackingstep

@idcol int
AS
SELECT * FROM step
WHERE idcol = @idcol
GO
IF OBJECT_ID('dbo.wsgsp_gettrackingcodedata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_gettrackingcodedata
GO
CREATE PROCEDURE wsgsp_gettrackingcodedata

AS

SELECT * FROM step
ORDER BY code
GO

IF OBJECT_ID('dbo.wsgsp_gettrackingstepbycode') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_gettrackingstepbycode
GO
CREATE PROCEDURE wsgsp_gettrackingstepbycode

@code char (5)
AS
SELECT * FROM step where code = @code
GO


IF OBJECT_ID('dbo.view_stepsubscriberdata') IS NOT NULL
  DROP VIEW dbo.view_stepsubscriberdata
  GO
CREATE VIEW view_stepsubscriberdata
AS
SELECT userid, username, emailaddress, s.idcol,s.stepid  FROM appuser  
INNER JOIN stepsubscriber s
ON appuser.idcol = s.stepuserid 
GO
IF OBJECT_ID('dbo.wsgsp_getsubscribers') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsubscribers
  GO
CREATE PROCEDURE wsgsp_getsubscribers
@stepid int
AS 
select * FROM view_stepsubscriberdata 
 where stepid = @stepid 
GO

IF OBJECT_ID('dbo.wsgsp_getnonsubscribers') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getnonsubscribers
  GO
CREATE PROCEDURE wsgsp_getnonsubscribers
@stepid int
AS 
select * from appuser
LEFT OUTER JOIN stepsubscriber
ON appuser.idcol = stepsubscriber.stepuserid 
 where NOT exists 
(SELECT stepuserid from stepsubscriber where stepuserid = appuser.idcol AND stepsubscriber.stepid = @stepid )
ORDER BY userid
GO

IF OBJECT_ID('dbo.wsgsp_deletesubscription') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deletesubscription
  GO
CREATE PROCEDURE wsgsp_deletesubscription
@idcol int
AS  
  DELETE FROM stepsubscriber WHERE idcol = @idcol
     
 GO

IF OBJECT_ID('dbo.wsgsp_deletetrackingstep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deletetrackingstep
  GO
CREATE PROCEDURE wsgsp_deletetrackingstep
 @idcol INT,
 @deletemessage char(12)OUT
AS  
IF EXISTS	 (SELECT top 1 * FROM jtrak where stepid = @idcol ORDER BY sono) 
  SELECT @deletemessage = 'Step in use' 
else
  BEGIN
    DELETE from step WHERE idcol = @idcol
    select @deletemessage = 'Step Deleted' 
 END 
GO

IF OBJECT_ID('dbo.wsgsp_insertroutestep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_insertroutestep
  GO
CREATE PROCEDURE wsgsp_insertroutestep
@route int,
@stepid int,
@userid char(4),
@insertmessage char(20)OUT
AS  
IF EXISTS	 (SELECT top 1 * FROM route where route = @route AND stepid = @stepid) 
  SELECT @insertmessage = 'Duplicate Step' 
else
  BEGIN
    INSERT INTO route ( route, stepid, custstep, adduser,adddate,lckuser,lckdate, lckstat)
     VALUES (@route, @stepid,'N', @userid, GETDATE(),@userid, GETDATE(),'') 
   select @insertmessage = 'Step Inserted' 

 END

GO


IF OBJECT_ID('dbo.wsgsp_getroutedata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getroutedata
  GO
CREATE PROCEDURE wsgsp_getroutedata
@route int
AS
SELECT * FROM view_routedata WHERE route = @route order by code 
GO

IF OBJECT_ID('dbo.wsgsp_deleteroutestep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deleteroutestep
  GO
CREATE PROCEDURE wsgsp_deleteroutestep
@route int,
@stepid int
AS  
  DELETE FROM route WHERE route = @route AND stepid = @stepid

GO

IF OBJECT_ID('dbo.wsgsp_getworkgroups') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getworkgroups
  GO
CREATE PROCEDURE wsgsp_getworkgroups
AS  
  SELECT * FROM workgroup ORDER BY groupname 

GO
IF OBJECT_ID('dbo.wsgsp_getsingleworkgroup') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingleworkgroup
  GO
CREATE PROCEDURE wsgsp_getsingleworkgroup
@idcol int
AS  
  SELECT * FROM workgroup WHERE idcol = @idcol
GO

IF OBJECT_ID('dbo.wsgsp_getworkgroupstepdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getworkgroupstepdata
  GO
CREATE PROCEDURE wsgsp_getworkgroupstepdata
@workgroupid int
AS
SELECT * FROM view_workgroupstepdata WHERE workgroupid = @workgroupid
 order by code 
GO

IF OBJECT_ID('dbo.wsgsp_deleteworkgroupstep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_deleteworkgroupstep
  GO
CREATE PROCEDURE wsgsp_deleteworkgroupstep
@idcol int
AS  
 DELETE workgroupstep WHERE   idcol = @idcol
  
GO

IF OBJECT_ID('dbo.wsgsp_getsingleworkgroupstep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingleworkgroupstep
  GO
CREATE PROCEDURE wsgsp_getsingleworkgroupstep
@idcol int
AS  
 SELECT * FROM workgroupstep WHERE   idcol = @idcol
  
GO

IF OBJECT_ID('dbo.wsgsp_insertworkgroupstep') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_insertworkgroupstep
  GO
CREATE PROCEDURE wsgsp_insertworkgroupstep
@workgroup int,
@stepid int,
@userid char(4),
@insertmessage char(20)OUT
AS  
IF EXISTS	 (SELECT top 1 * FROM workgroupstep where 
  workgroupid = @workgroup AND stepid = @stepid) 
  SELECT @insertmessage = 'Duplicate Step' 
else
  BEGIN
    INSERT INTO workgroupstep ( workgroupid, stepid, adduser,adddate)
     VALUES (@workgroup, @stepid,@userid, GETDATE()) 
   select @insertmessage = 'Step Inserted' 

 END

GO
IF OBJECT_ID('dbo.wsgsp_getappusers') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getappusers
  GO
CREATE PROCEDURE wsgsp_getappusers
AS

SELECT * from appuser 
ORDER BY userid

GO

IF OBJECT_ID('dbo.wsgsp_getsingleappuser') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsingleappuser
  GO
CREATE PROCEDURE wsgsp_getsingleappuser
@idcol int
AS  
 SELECT * from appuser WHERE idcol = @idcol
GO

IF OBJECT_ID('dbo.wsgsp_getappuserbyuserid') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getappuserbyuserid
  GO
CREATE PROCEDURE wsgsp_getappuserbyuserid
@userid char(4)
AS  
SELECT * FROM appuser where userid = @userid
GO

IF OBJECT_ID('dbo.wsgsp_getsotracking') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getsotracking
  GO
CREATE PROCEDURE wsgsp_getsotracking
@sono char (10)

AS
SELECT * FROM view_trackingstepdata
WHERE  sono = @sono AND cancelled <> 1 ORDER BY adddate DESC
GO

IF OBJECT_ID('dbo.view_trackingstepdata') IS NOT NULL
  DROP VIEW dbo.view_trackingstepdata
  GO

CREATE VIEW view_trackingstepdata
AS

SELECT  a.trackdate, a.idcol, a.adddate, a.adduser, a.sono,b.code,a.comment, a.cancelled, a.stepid, a.seq, a.upstrack,a.sentmail,a.shipped,a.prevdate,
b.ondescrip,b.descrip,b.filetosend,b.sendftp,b.inspection, b.alertinterval, b.alertmessage, c.company, c.sodate, c.ponum,
c.ordate, c.lname
FROM jtrak a
INNER JOIN step b
ON a.stepid = b.idcol
INNER join view_somastdata c ON a.sono = c.sono 
LEFT JOIN view_inspmastexpanded d ON a.SoNo = d.sono
GO

IF OBJECT_ID('dbo.view_expandedroutedata') IS NOT NULL
  DROP VIEW dbo.view_expandedroutedata
  GO

CREATE VIEW view_expandedroutedata
AS

SELECT c.code AS routecode,c.descrip AS routename,c.descrip + ' - ' + b.descrip AS fullroutestepname, b.code as routestepcode, b.descrip AS routestepname, a.route, a.idcol, a.custstep, a.stepid FROM route a

INNER JOIN step b ON b.idcol = a.stepid 
INNER JOIN step c on c.idcol = a.route
GO


IF OBJECT_ID('dbo.view_latestsotrackingstepdata') IS NOT NULL
  DROP VIEW dbo.view_latestsotrackingstepdata
  GO

CREATE VIEW view_latestsotrackingstepdata
AS

SELECT t.*,  COALESCE(i.locationname, SPACE(15)) AS location, s.invno,s.custno,s.sostat,s.sotype,s.lckstat,s.lckuser, s.ponum, s.sodate,s.meycono, s.ordate, s.lname, c.company FROM (
SELECT a.* FROM  (
select sono, stepid,code, seq, idcol, upstrack,inspection,alertinterval,alertmessage,ondescrip, adduser, descrip, comment,
trackdate,  row_number() over (partition by sono order by seq DESC) as RowNum from view_trackingstepdata
WHERE trackdate > GETDATE() -360 AND cancelled <> 1) a WHERE rownum = 1) t
INNER JOIN somast s
ON t.sono = s.sono
INNER join arcust c ON s.custno = c.custno
LEFT JOIN view_inspmastexpanded i ON t.sono = i.sono
GO

IF OBJECT_ID('dbo.wsgsp_getlatestsotrackingstepdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getlatestsotrackingstepdata
  GO
CREATE PROCEDURE wsgsp_getlatestsotrackingstepdata
@stepid int

AS
SELECT * FROM view_latestsotrackingstepdata
WHERE stepid = @stepid ORDER BY sono
GO

IF OBJECT_ID('dbo.wsgsp_getalllatestsotrackingstepdata') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getalllatestsotrackingstepdata
  GO
CREATE PROCEDURE wsgsp_getalllatestsotrackingstepdata

AS
SELECT * FROM view_latestsotrackingstepdata
GO

IF OBJECT_ID('dbo.wsgsp_GetLatestTrackingStepsbyStepid') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_GetLatestTrackingStepsbyStepid
  GO
CREATE PROCEDURE wsgsp_GetLatestTrackingStepsbyStepid
@stepid int
AS
SELECT * FROM  view_latestsotrackingstepdata
WHERE stepid = @stepid ORDER BY trackdate
GO



IF OBJECT_ID('dbo.wsgp_GetLatestTrackingStepsbyWorkgroupId') IS NOT NULL
  DROP PROCEDURE dbo.wsgp_GetLatestTrackingStepsbyWorkgroupId
  GO
CREATE PROCEDURE wsgp_GetLatestTrackingStepsbyWorkgroupId
@workgroupid int
AS
SELECT * FROM  view_latestsotrackingstepdata
WHERE stepid IN 
(select stepid FROM workgroupstep WHERE workgroupid = @workgroupid) 
order by trackdate, sono, code 
GO

IF OBJECT_ID('dbo.wsgsp_unlocktablerow') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_unlocktablerow
  GO
CREATE PROCEDURE wsgsp_unlocktablerow

@tablename CHAR(40),
@idcol int

AS
DECLARE @sql NVARCHAR(250)
DECLARE @charid CHAR(7)
SET @charid = CAST( @idcol AS varchar)
SET @tablename = RTRIM(@tablename) 
SET @sql = N'UPDATE ' + @tablename + ' SET lckstat = SPACE(2)  where idcol  = ' + @charid
EXEC sp_executesql @sql
GO

IF OBJECT_ID('dbo.wsgsp_getinsplocation') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getinsplocation
  GO
CREATE PROCEDURE wsgsp_getinsplocation
@sono char(10)
AS  
 SELECT * FROM view_inspmastexpanded WHERE sono = @sono
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[wsgsp_searchlatestsomastracking]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[wsgsp_searchlatestsomastracking]
  
GO
 CREATE PROCEDURE [dbo].[wsgsp_searchlatestsomastracking]	
@sono varchar(10),
@ponum varchar(20),
@invno varchar(10),
@custno varchar(6),
@lname varchar(20),
@meycono varchar(10),
@begindate datetime,
@enddate datetime

AS	
SELECT @sono = RTRIM(@sono) + '%'
SELECT @invno =  RTRIM(UPPER(@invno)) + '%'
SELECT @ponum = RTRIM(UPPER(@ponum)) + '%'
SELECT @custno = RTRIM(UPPER(@custno)) + '%' 
SELECT @lname = RTRIM(UPPER(@lname)) + '%' 
SELECT @meycono = RTRIM(UPPER(@meycono)) + '%' 

SELECT *, '  /  /    ' AS procdate,SPACE(5) as trakcode, 'N' AS hascomment from view_latestsotrackingstepdata 
where LTRIM(sono) LIKE  @sono
AND UPPER(ponum) LIKE @ponum  
AND LTRIM(invno) LIKE @invno  
AND UPPER(custno) LIKE @custno  
AND UPPER(lname) LIKE @lname  
AND UPPER(meycono) LIKE @meycono  
AND sodate BETWEEN @begindate AND @enddate
AND sostat <> 'V'
ORDER BY sono
GO

IF OBJECT_ID(N'dbo.GetUserName', N'FN') IS NOT NULL
    DROP FUNCTION dbo.GetUserName;
GO
CREATE FUNCTION dbo.GetUserName           -- function name
(@userid char(4))                     -- input parameter name and data type
RETURNS CHAR(100)                          -- return parameter data type
AS
BEGIN                                -- begin body definition
DECLARE @username CHAR(35)
IF EXISTS (select username from appuser where userid = @userid
 )
 
   SELECT @username = ( select username from appuser where userid = @userid)
ELSE
   SELECT @username = 'Unknown'
RETURN @username
END;
GO

IF OBJECT_ID('dbo.wsgsp_insertictranreceipt') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_insertictranreceipt
  GO
CREATE PROCEDURE wsgsp_insertictranreceipt
@tranno char(10),
@loctid char(6),
@item char(15),
@trantyp char (2),
@tdate datetime,
@sourcelineno int,
@tqty decimal(15,6),
@tnotes varchar(250),
@docno char(10),
@price decimal (15,5),
@meycono char(15),
@receiptid CHAR(50) OUTPUT,
@user CHAR(4)

AS

INSERT INTO ictran
(tranno, loctid, item,trantyp,tdate, tstat, sourcelineno,  tqty,  tnotes, docno, price, meycono,sewnonlabelprinted,adduser, adddate, lckstat, lckuser, lckdate) 
VALUES
(@tranno, @loctid,@item ,@trantyp, @tdate,'A', @sourcelineno,  @tqty,@tnotes,@docno,@price,@meycono,'N',@user, GETDATE(), '',  @user, GETDATE()  )
UPDATE ictran SET parenttransid = idcol WHERE idcol =  SCOPE_IDENTITY()
SET @receiptid = SCOPE_IDENTITY()
SELECT @receiptid 

GO

IF OBJECT_ID('dbo.view_itemloctidonhand') IS NOT NULL
  DROP VIEW dbo.view_itemloctidonhand
  GO
CREATE VIEW view_itemloctidonhand
AS


SELECT a.item, a.loctid, a.loctid as locdesc, b.descrip as itemdesc, dbo.GetItemLoctidAllocation(a.item,a.loctid) AS allocated,  SUM(a.tqty) as tqty, SUM(tqty * a.price) AS extprice FROM ictran a 
INNER JOIN immaster b ON a.item = b.item
WHERE a.tstat = 'A' AND trantyp <> 'H'
GROUP by a.item,a.loctid, a.loctid, b.descrip

GO

IF OBJECT_ID('dbo.view_expandedsoline') IS NOT NULL
  DROP VIEW dbo.view_expandedsoline
  GO
CREATE VIEW view_expandedsoline
AS

SELECT a.*, b.stocking, b.weight FROM soline a 
INNER JOIN immaster b ON a.item = b.item
GO


IF OBJECT_ID('dbo.view_itemloctidtdateonhand') IS NOT NULL
  DROP VIEW dbo.view_itemloctidtdateonhand
  GO
CREATE VIEW view_itemloctidtdateonhand
AS

SELECT a.item,c.descrip, a.loctid, a.loctid AS locdesc, a.price, a.parenttransid, a.tdate, a.adddate, b.tqty AS onhand FROM ictran a 
inner JOIN 
(SELECT item,loctid, parenttransid, SUM(tqty) as tqty from ictran WHERE tstat = 'A' group by item,loctid, parenttransid having SUM(tqty) > 0 )b
ON a.parenttransid = b.parenttransid
INNER JOIN immaster c ON a.item = c.item
WHERE a.trantyp = 'R' AND tstat = 'A'
GO

IF OBJECT_ID('dbo.wsgsp_getopenitemloctid') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_getopenitemloctid
  GO
CREATE PROCEDURE wsgsp_getopenitemloctid
@item char(15),
@loctid char(6)

AS
SELECT * FROM  view_itemloctidtdateonhand WHERE item = @item AND loctid = @loctid
ORDER BY tdate, adddate
GO


IF OBJECT_ID('dbo.wsgsp_insertictranstockcoverhold') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_insertictranstockcoverhold
  GO
CREATE PROCEDURE wsgsp_insertictranstockcoverhold
@loctid char(6),
@item char(15),
@tnotes varchar(250),
@meycono char(15),
@user CHAR(4)

AS

DECLARE @transid int = 0


INSERT INTO ictran
(tranno, loctid, item,trantyp,tdate, tstat, sourcelineno, parenttransid,  tqty,  tnotes, docno, price,meycono, sewnonlabelprinted,  adduser, adddate, lckstat, lckuser, lckdate) 
VALUES
('', @loctid,@item , 'H', GETDATE(),'A', 1, 0, 1, @tnotes,'Cover',0, @meycono, 'N',  @user, GETDATE(), '',  @user, GETDATE()  )

SET @transid = SCOPE_IDENTITY()
UPDATE ictran SET parenttransid = idcol WHERE idcol = @transid



GO



IF OBJECT_ID('dbo.wsgsp_insertictranissue') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_insertictranissue
  GO
CREATE PROCEDURE wsgsp_insertictranissue
@tranno char(10),
@loctid char(6),
@item char(15),
@trantyp char (2),
@tdate datetime,
@parenttransid int,
@sourcelineno int,
@tqty decimal(15,6),
@tnotes varchar(250),
@docno char(10),
@price decimal (15,5),
@meycono char(15),
@user CHAR(4)

AS

INSERT INTO ictran
(tranno, loctid, item,trantyp,tdate, tstat, sourcelineno, parenttransid,  tqty,  tnotes, docno, price,meycono, sewnonlabelprinted,  adduser, adddate, lckstat, lckuser, lckdate) 
VALUES
('', @loctid,@item ,@trantyp, @tdate,'A', @sourcelineno, @parenttransid,  @tqty, @tnotes,@docno,@price, @meycono, 'N', @user, GETDATE(), '',  @user, GETDATE()  )

GO

IF OBJECT_ID('dbo.wsgsp_insertictranreceiptandissue') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_insertictranreceiptandissue
  GO
CREATE PROCEDURE wsgsp_insertictranreceiptandissue

@tranno char(10),
@loctid char(6),
@item char(15),
@trantyp char (2),
@tdate datetime ,
@sourcelineno int, 
@tqty decimal(15,6),
@tnotes varchar(250),
@docno char(10),
@price decimal (15,5),
@meycono char(15),
@user CHAR(4)

AS

DECLARE @receiptid int = 0

INSERT INTO ictran
(tranno, loctid, item,trantyp,tdate, tstat, sourcelineno,  tqty,  tnotes, docno, price, meycono, sewnonlabelprinted, adduser, adddate, lckstat, lckuser, lckdate) 
VALUES
('', @loctid,@item ,@trantyp, @tdate,'A', @sourcelineno,  @tqty,@tnotes,@docno,@price, @meycono, 'N', @user, GETDATE(), '',  @user, GETDATE()  )
SET @receiptid = SCOPE_IDENTITY()
UPDATE ictran SET parenttransid = idcol WHERE idcol = @receiptid
SET @trantyp = 'I'
SET @tqty = - @tqty

INSERT INTO ictran
(tranno, loctid, item,trantyp,tdate, tstat, sourcelineno,  tqty,  tnotes, docno, price, meycono, sewnonlabelprinted, parenttransid, adduser, adddate, lckstat, lckuser, lckdate) 
VALUES
('', @loctid,@item ,@trantyp, @tdate,'A', @sourcelineno,  @tqty,@tnotes,@docno,@price, @meycono, 'N',  @receiptid, @user, GETDATE(), '',  @user, GETDATE()  )

GO


IF OBJECT_ID('dbo.view_expandedictran') IS NOT NULL
  DROP VIEW dbo.view_expandedictran
  GO
CREATE VIEW view_expandedictran
AS

SELECT  b.descrip AS itmdesc, a.loctid AS locdesc, a.* FROM view_consolidatedinventoryactivity a
INNER JOIN immaster b ON a.item = b.item
GO

IF OBJECT_ID('dbo.view_openordersitemsonhand') IS NOT NULL
  DROP VIEW dbo.view_openordersitemsonhand
  GO
CREATE VIEW view_openordersitemsonhand
AS



SELECT a.sono, a.item, a.itmdesc,dbo.GetItemLoctidOnHand(a.item,b.defloc) AS onhand,  b.defloc, a.qtyact, b.sotype  from view_coverandlines a
INNER JOIN somast b ON a.sono = b.sono
WHERE a.stocking = 1 AND b.sotype = 'O' AND b.sostat <> 'V' AND b.sostat <> 'C'
AND  EXISTS( SELECT * FROM view_itemloctidonhand WHERE item = a.item AND loctid = b.defloc ) 
GO


IF OBJECT_ID('dbo.view_openordersitemloctid') IS NOT NULL
  DROP VIEW dbo.view_openordersitemloctid
  GO
CREATE VIEW view_openordersitemloctid
AS
SELECT a.sono,a.item,a.itmdesc, b.defloc, a.qtyact, b.sotype,  0 AS onhand from view_coverandlines a
INNER JOIN somast b ON a.sono = b.sono
WHERE a.stocking = 1 AND b.sotype = 'O' AND b.sostat <> 'V' AND b.sostat <> 'C'
GO

IF OBJECT_ID('dbo.wsgsp_findviewexpandedsolinedatabysono') IS NOT NULL
  DROP PROCEDURE dbo.wsgsp_findviewexpandedsolinedatabysono
  GO
CREATE PROCEDURE wsgsp_findviewexpandedsolinedatabysono

@sono CHAR(10)
AS
SELECT * FROM View_ExpandedSoline WHERE sono = @sono   
GO

IF OBJECT_ID('dbo.view_openorderstockitems') IS NOT NULL
  DROP VIEW dbo.view_openorderstockitems
  GO
CREATE VIEW view_openorderstockitems
AS
SELECT a.item, a.qtyact , a.descrip,b.* FROM view_coverandlines a
INNER JOIN somast b ON a.sono = b.sono
WHERE a.stocking = 1 AND b.sotype = 'O' AND b.sostat <> 'V'  AND   b.sostat <> 'C'   
GO

IF OBJECT_ID('dbo.wsgsp_getbatchinvoices') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchinvoices
  GO
CREATE PROCEDURE wsgsp_getbatchinvoices
AS

select sono, b.location, b.code, b.idcol, x.adddate
 from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, adddate,
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printinvoice = 'Y'
ORDER BY adddate DESC
GO

IF OBJECT_ID('dbo.wsgsp_getbatchworkorders') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchworkorders
  GO
CREATE PROCEDURE wsgsp_getbatchworkorders
AS

select sono, b.location, b.code, b.idcol,X.adddate
 from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, adddate,
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printworkorder = 'Y'
ORDER BY adddate DESC
GO

IF OBJECT_ID('dbo.wsgsp_getbatchpacklists') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchpacklists
  GO
CREATE PROCEDURE wsgsp_getbatchpacklists
AS

select sono, b.location, b.code, b.idcol , x.adddate
from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, adddate,
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printpacklist = 'Y'
ORDER BY adddate DESC
GO

IF OBJECT_ID('dbo.wsgsp_getbatchsewnonlabels') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchsewnonlabels
  GO
CREATE PROCEDURE wsgsp_getbatchsewnonlabels
AS
select sono, b.location, b.code, b.idcol, X.adddate  from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, adddate,
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printsewnonlabel = 'Y'
ORDER BY adddate DESC
GO

IF OBJECT_ID('dbo.wsgsp_getbatchidentitylabels') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchidentitylabels
  GO
CREATE PROCEDURE wsgsp_getbatchidentitylabels
AS
select sono, b.location, b.code, b.idcol ,x.adddate 
from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, adddate,
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printidentitylabel = 'Y'
ORDER BY adddate DESC
GO


IF OBJECT_ID('dbo.view_latestsotrack') IS NOT NULL
  DROP VIEW dbo.view_latestsotrack
  GO
CREATE VIEW view_latestsotrack
AS
select sono, b.location, b.code, b.idcol from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 
GO

IF OBJECT_ID('dbo.view_customertransactions') IS NOT NULL
  DROP VIEW dbo.view_customertransactions
  GO
CREATE VIEW view_customertransactions
AS
SELECT  'C' as recordtype,  c.sono, o.custno, o.company, o.ponum, o.meycono, o.ornum, o.enterqu, o.sotype,o.sodate, o.ordate, o.produnits, o.invno, o.invdte, c.version, c.material, c.color, c.spacing, c.overlap , c.coverstring, c.poolstring, c.product,c.descrip,c.item,c.itmdesc , t.code, t.descrip AS trackdescrip,  t.trackdate, o.ordamt,
o.tax, o.shpamt
FROM (SELECT sono, custno, company, ponum,meycono, sodate, ordate, enterqu, ornum, ordamt,tax, shpamt, produnits, invno, invdte,  sostat, sotype FROM view_somastdata) o
INNER JOIN (SELECT * FROM  view_coverdata where covertype = 'C') c
ON c.sono = o.sono
INNER JOIN (SELECT ordamt , sono, tax, shipping, version   FROM soversion) V 
ON c.sono = v.sono AND c.version = v.version
INNER JOIN view_solasttrackingdata T
ON t.sono = c.sono
WHERE o.sostat <> 'V' and o.enterqu = 'Y' and o.sotype = 'O'
UNION ALL
SELECT 'M' as recordtype, view_somastdata.sono, custno,company,ponum, meycono, ornum, enterqu,sotype, sodate, ordate, produnits, invno, invdte, ' ' AS version,  ' ' AS  material, ' ' AS color, ' ' AS spacing, ' ' AS overlap ,
' ' AS coverstring,  ' ' AS poolstring, 'Misc' AS product , 'Miscellaneous Order' AS descrip, ' ' AS item,
' ' AS itmdesc ,  t.code, t.descrip AS trackdescrip,  t.trackdate, ordamt, tax, shpamt
FROM  view_somastdata
INNER JOIN view_solasttrackingdata t
ON t.sono = view_somastdata.sono
 WHERE sostat <> 'V' and enterqu = 'N' and sotype = 'O'
UNION ALL 
 SELECT  'E' as recordtype,view_somastdata.sono, custno,company,ponum, meycono, ornum, enterqu,sotype, sodate, ordate, produnits, invno, invdte, ' ' AS version,  ' ' AS  material, ' ' AS color, ' ' AS spacing, ' ' AS overlap ,
' ' AS coverstring,  ' ' AS poolstring, 'Misc' AS product , 'Cover Estimate' AS descrip, ' ' AS item,
' ' AS itmdesc ,  t.code, t.descrip AS trackdescrip,  t.trackdate, ordamt, tax, shpamt
FROM  view_somastdata
INNER JOIN view_solasttrackingdata t
ON t.sono = view_somastdata.sono
 WHERE sostat <> 'V' and enterqu = 'Y' and sotype = 'B'
GO
IF OBJECT_ID('dbo.view_expandedcusthwpref') IS NOT NULL
  DROP VIEW dbo.view_expandedcusthwpref
  GO
CREATE VIEW view_expandedcusthwpref
AS


SELECT a.*, b.shortdescrip FROM 
custhwpref a
INNER JOIN
 (SELECT * FROM view_immasterdata where LEFT(misccode,2) = 'HW') b
ON a.item = b.item
 
GO

IF OBJECT_ID('dbo.view_expandedincident') IS NOT NULL
  DROP VIEW dbo.view_expandedincident
  GO
CREATE VIEW view_expandedincident
AS
SELECT        a.sono, a.incidentdate, a.issueid, a.findingdeptid, a.causingdeptid, a.cost, a.notes, a.employeeid, a.rootcauseid, a.resolutionid, a.adduser, a.adddate, a.idcol, a.lckuser, a.lckdate, a.lckstat, b.custno, 
                         dbo.GetRefDescription(a.employeeid) AS employee, dbo.GetRefDescription(a.issueid) AS issue, dbo.GetRefDescription(a.findingdeptid) AS findingdept, dbo.GetRefDescription(a.causingdeptid) AS causingdept, 
                         dbo.GetRefDescription(a.rootcauseid) AS rootcause, dbo.GetRefDescription(a.resolutionid) AS resolution
FROM            dbo.incident AS a INNER JOIN
                         dbo.somast AS b ON a.sono = b.sono

GO

IF OBJECT_ID('dbo.view_refdescription') IS NOT NULL
  DROP VIEW dbo.view_refdescription
  GO
CREATE VIEW view_refdescription
AS

SELECT *, dbo.GetRefDescription(0) AS refdescription FROM sysreference
GO

IF OBJECT_ID('dbo.view_expandedticket') IS NOT NULL
  DROP VIEW dbo.view_expandedticket
  GO
CREATE VIEW view_expandedticket
AS
SELECT dbo.GetContactName(contactid) AS contactname, ROW_NUMBER() OVER (ORDER BY idcol  ASC) AS sequno, * from ticket
GO 

IF OBJECT_ID('dbo.view_consolidatedinventoryactivity') IS NOT NULL
  DROP VIEW dbo.view_consolidatedinventoryactivity
  GO
CREATE VIEW view_consolidatedinventoryactivity
AS

select item, loctid, tdate, trantyp,tqty,price,meycono,sewnonlabelprinted, docno from ictran where tstat = 'A'
UNION ALL
select item, locid AS loctid, date AS tdate, trantype AS trantyp, qty AS tqty, cost AS price, space(15) as meycono, space(1) AS sewnonlabelprinter, docid AS docno FROM
(select a.item, trantype, a.locid,date, qty, docid, cost from imactlog a 
INNER join immaster m ON a.item = m.item
where a.doctype = 'PO' and m.stocking = 1) a
GO


IF OBJECT_ID('dbo.sp_marktrackingeventmailsent') IS NOT NULL
  DROP PROCEDURE dbo.sp_marktrackingeventmailsent
  GO
CREATE PROCEDURE sp_marktrackingeventmailsent
@idcol int,
@userid char(4)
AS  
  -- Set sentmail to 1
  UPDATE jtrak SET sentmail = 1 , lckuser = @userid, lckdate = GETDATE() WHERE idcol = @idcol

GO



IF OBJECT_ID('dbo.sp_getnonsubscribers') IS NOT NULL
  DROP PROCEDURE dbo.sp_getnonsubscribers
  GO
CREATE PROCEDURE sp_getnonsubscribers
@stepid int
AS 
select * from appuser
LEFT OUTER JOIN stepsubscriber
ON appuser.idcol = stepsubscriber.stepuserid 
 where NOT exists 
(SELECT stepuserid from stepsubscriber where stepuserid = appuser.idcol AND stepsubscriber.stepid = @stepid )
GO


IF OBJECT_ID('dbo.sp_insertlogevent') IS NOT NULL
  DROP PROCEDURE dbo.sp_insertlogevent
  GO
CREATE PROCEDURE sp_insertlogevent
@eventdesc nvarchar(50),
@eventresult nvarchar(max),
@userid char(4)
AS  
    INSERT INTO eventlog ( eventdesc, eventresult, eventtime,eventuser)
     VALUES (@eventdesc, @eventresult,GETDATE(),@userid) 
 GO


IF OBJECT_ID('dbo.sp_getupstracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_getupstracking
  GO
CREATE PROCEDURE sp_getupstracking           
@sono CHAR(10)
AS
select * from jtrak where upstrack <> ' ' 
AND sono = @sono  
GO
     
IF OBJECT_ID('dbo.sp_getlatesttracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_getlatesttracking
  GO
CREATE PROCEDURE sp_getlatesttracking
AS

select sono, b.code,sentmail, b.ondescrip, b.message,b.sendso,  b.sendftp, b.filetosend, b.alertinterval, b.alertmessage, b.internalmail, X.idcol, X.stepid, upstrack, trackdate,seq from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1
ORDER BY sono

GO

IF OBJECT_ID('dbo.sp_extracttrackingbytime') IS NOT NULL
  DROP PROCEDURE dbo.sp_extracttrackingbytime
  GO
CREATE PROCEDURE sp_extracttrackingbytime
@extractinterval int
AS
SELECT DISTINCT sono, sendftp   FROM view_trackingstepdata 
WHERE  DATEDIFF(hh, trackdate , GETDATE()) < @extractinterval and sendftp = 1 and trackdate <= GETDATE()
ORDER BY sono 

GO

IF OBJECT_ID('dbo.sp_updatejtraksentmail') IS NOT NULL
  DROP PROCEDURE dbo.sp_updatejtraksentmail
  GO
CREATE PROCEDURE sp_updatejtraksentmail
@idcol int,
@sentmail bit,
@userid char (4)
AS  
    UPDATE jtrak SET sentmail = @sentmail, lckuser = @userid,
    lckdate = GETDATE() WHERE idcol = @idcol 
   
GO


IF OBJECT_ID('dbo.sp_getworkgrouptracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_getworkgrouptracking
  GO
CREATE PROCEDURE sp_getworkgrouptracking
@workgroupid int
AS

select x.sono, '          ' AS  sodate, '          ' AS ordate,

'123456789012345678901234567890' AS company, code, trackdate,
'               ' AS ponum,
'                        ' AS lname,

'          ' AS proctime,
hascomment =
CASE
WHEN EXISTS (SELECT * from jtrak WHERE sono = x.sono and  comment <> ' ') THEN 'Y'
ELSE  'N'
END,

 trackdate,
  x.stepid,
  comment,
 '      ' AS custno,seq from
  (select SoNo, stepid, code, seq, trackdate, comment, 
row_number() over (partition by sono order by seq DESC) 
as RowNum from view_trackingstepdata 
WHERE trackdate > GETDATE() -180 AND cancelled <> 1 ) X 
where RowNum = 1 AND trackdate > GETDATE() -180 
AND stepid IN 
(select stepid FROM workgroupstep WHERE workgroupid = @workgroupid) 
order by x.trackdate, stepid 
GO



IF OBJECT_ID('dbo.sp_getworkgroupstepdata') IS NOT NULL
  DROP PROCEDURE dbo.sp_getworkgroupstepdata
  GO
CREATE PROCEDURE sp_getworkgroupstepdata
@workgroupid int
AS
SELECT * FROM view_workgroupstepdata WHERE workgroupid = @workgroupid
 order by code 
GO

IF OBJECT_ID('dbo.view_workgroupstepdata') IS NOT NULL
  DROP VIEW dbo.view_workgroupstepdata
  GO

CREATE view view_workgroupstepdata
AS

SELECT b.code, b.descrip,a.idcol, a.stepid,a.workgroupid FROM workgroupstep a
INNER JOIN step b ON b.idcol = a.stepid
GO


IF OBJECT_ID('dbo.view_taxdistrictrate') IS NOT NULL
  DROP VIEW dbo.view_taxdistrictrate
  GO

CREATE view view_taxdistrictrate
AS

select d.*, r.taxrate from cotaxtbl d inner join cotxrate r on d.state = r.rateid where r.delmark = 0 and d.delmark = 0

GO


IF OBJECT_ID('dbo.sp_getworkgroups') IS NOT NULL
  DROP PROCEDURE dbo.sp_getworkgroups
  GO
CREATE PROCEDURE sp_getworkgroups
AS  
  SELECT * FROM workgroup ORDER BY groupname 

GO



IF OBJECT_ID('dbo.sp_getsuspendstepID') IS NOT NULL
  DROP PROCEDURE dbo.sp_getsuspendstepID
  GO
CREATE PROCEDURE sp_getsuspendstepID

AS

select idcol from step where code = 'SUSP' 

GO

IF OBJECT_ID('dbo.sp_getinitID') IS NOT NULL
  DROP PROCEDURE dbo.sp_getinitID
  GO
CREATE PROCEDURE sp_getinitID

AS

select idcol from step where code = 'INIT' 

GO


IF OBJECT_ID('dbo.sp_getsorangetracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_getsorangetracking
  GO
CREATE PROCEDURE sp_getsorangetracking

@beginsono char (10),
@endsono char (10)

AS

select sono, b.code,trackdate,
seq, stepid, hascomment from (select SoNo, stepid, seq, trackdate, hascomment =
CASE
WHEN EXISTS (SELECT * from jtrak WHERE sono = a.sono AND comment <> ' ') THEN 'Y'
ELSE  'N'
END,
row_number() over (partition by sono  order by adddate DESC ) as RowNum from jtrak a WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1
AND sono >= @beginsono AND sono <= @endsono 
order by sono
GO
IF OBJECT_ID('dbo.sp_getsotracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_getsotracking
  GO
CREATE PROCEDURE sp_getsotracking
@sono char (10)

AS

select a.trackdate,a.stepid,b.code,a.adddate, comment,a.adduser,a.upstrack,b.descrip,a.idcol,seq, sentmail  from jtrak a 
INNER join step  b 
ON a.stepid = b.idcol 
WHERE  sono = @sono AND cancelled <> 1 ORDER BY adddate DESC



GO

IF OBJECT_ID('dbo.sp_getnextcodetracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_getnextcodetracking
  GO
CREATE PROCEDURE sp_getnextcodetracking

@stepid int

AS
select x.sono, '          ' AS  sodate, '          ' AS ordate,

'123456789012345678901234567890' AS company,code, trackdate,
'               ' AS ponum,
'                        ' AS lname,
hascomment =
CASE
WHEN EXISTS (SELECT * from jtrak WHERE sono = x.sono AND stepid = @stepid  and  comment <> ' ') THEN 'Y'
ELSE  'N'
END,

'          ' AS proctime,
trackdate, x.stepid, x.comment, 
 '      ' AS custno,seq from (select SoNo, stepid,code, seq, comment,
trackdate, row_number() over (partition by sono order by seq DESC) as RowNum from view_trackingstepdata
WHERE trackdate > GETDATE() -180 AND cancelled <> 1 ) X 
where RowNum = 1 AND x.stepid = @stepid AND trackdate > GETDATE() -180
order by x.trackdate 



GO


IF OBJECT_ID('dbo.sp_gettrackingcode') IS NOT NULL
  DROP PROCEDURE dbo.sp_gettrackingcode
  GO
CREATE PROCEDURE sp_gettrackingcode

AS

SELECT code, descrip, idcol, soclose, ondescrip, internalmail, message, alertmessage,
 alertinterval, filetosend,sendftp,
  alertmessage, alertinterval FROM step
ORDER BY code

GO

IF OBJECT_ID('dbo.sp_getsingletrackingstep') IS NOT NULL
  DROP PROCEDURE dbo.sp_getsingletrackingstep
  GO
CREATE PROCEDURE sp_getsingletrackingstep
@idcol int

AS

SELECT code, descrip, idcol, soclose, ondescrip,internalmail,maxcode, alertinterval,alertmessage,
  message,filetosend,printinvoice, printworkorder, printpacklist,location, sendso,sendftp FROM step
WHERE idcol = @idcol

GO


IF OBJECT_ID('dbo.view_routedata') IS NOT NULL
  DROP VIEW dbo.view_routedata
  GO

CREATE view view_routedata
AS


SELECT b.code, b.descrip, a.route, b.mustbeinvoiced, a.idcol, a.custstep, a.stepid FROM route a
INNER JOIN step b ON b.idcol = a.stepid 
GO


IF OBJECT_ID('dbo.sp_getroutedata') IS NOT NULL
  DROP PROCEDURE dbo.sp_getroutedata
  GO
CREATE PROCEDURE sp_getroutedata
@route int
AS
SELECT * FROM view_routedata WHERE route = @route order by code 
GO




IF OBJECT_ID('dbo.sp_inserttrackingevent') IS NOT NULL
  DROP PROCEDURE dbo.sp_inserttrackingevent
  GO
CREATE PROCEDURE sp_inserttrackingevent
@stepid int,
@sono char(10),
@trackdate datetime,
@comment varchar(max),
@userid char(4)
AS  
DECLARE @seq numeric 
DECLARE @prevdate DATETIME
DECLARE @priorseq numeric
SET @seq = (select MAX(seq) from jtrak WHERE sono = @sono)
IF  @seq IS NULL
 BEGIN
  SET @seq = 1 
 END
ELSE
 SET @seq = @seq + 1
IF  @seq > 1
  BEGIN
   SET @priorseq = @seq - 1
   SET @prevdate = (select trackdate from jtrak WHERE sono = @sono and seq = @priorseq AND cancelled <> 1 )
   SELECT @prevdate 
  END 
ELSE
  SET @prevdate = GETDATE()


INSERT into jtrak (sono,trackdate,seq,prevdate,comment, adduser, adddate,lckuser, lckdate, sentmail,shipped,stepid,upstrack,cancelled)
VALUES (@sono,@trackdate,@seq,@prevdate,@comment,@userid, GETDATE(),@userid, GETDATE(),0,0,@stepid,' ',0)


GO



IF OBJECT_ID('dbo.sp_updatetrackingcomment') IS NOT NULL
  DROP PROCEDURE dbo.sp_updatetrackingcomment
  GO
CREATE PROCEDURE sp_updatetrackingcomment
@idcol int,
@comment varchar(max),
@userid char(4)
AS  
  UPDATE jtrak SET comment = @comment, lckuser = @userid, lckdate = GETDATE()
   WHERE idcol = @idcol


GO

IF OBJECT_ID('dbo.sp_resequencesotracking') IS NOT NULL
  DROP PROCEDURE dbo.sp_resequencesotracking
  GO
CREATE PROCEDURE sp_resequencesotracking
@sono char(10)
AS  
-- Recreate sequence numbers
UPDATE jtrak SET seq = b.newseq
FROM jtrak a
INNER JOIN (SELECT sono, adddate, idcol, ROW_NUMBER() OVER
 (PARTITION BY Sono ORDER BY AddDate,idcol) AS newseq
   FROM jtrak) b
ON a.idcol    = b.idcol where a.sono = @sono

-- Restore the previous date field
UPDATE jtrak 
SET prevdate = b.adddate
FROM  jtrak 
   INNER JOIN jtrak b 
    ON (jtrak.sono  = b.sono AND jtrak.seq = b.seq +1 )
    WHERE jtrak.sono = @sono and jtrak.seq <> 1
UPDATE jtrak 
SET prevdate = adddate
WHERE jtrak.sono = @sono and jtrak.seq = 1
 
GO


GO


IF OBJECT_ID('dbo.sp_canceltrackingevent') IS NOT NULL
  DROP PROCEDURE dbo.sp_canceltrackingevent
  GO
CREATE PROCEDURE sp_canceltrackingevent
@idcol int,
@userid char(4)
AS  
  -- Mark the event as cancelled
  UPDATE jtrak SET cancelled = 1 , lckuser = @userid, lckdate = GETDATE()
  WHERE idcol = @idcol
  -- Resequence the tracking events  
  DECLARE @sono char(10)
  SET @sono = (SELECT sono from jtrak WHERE idcol = @idcol)
  SELECT @sono 
  EXEC sp_resequencesotracking @sono
GO



IF OBJECT_ID('dbo.sp_applogin') IS NOT NULL
  DROP PROCEDURE dbo.sp_applogin
  GO

CREATE PROCEDURE sp_applogin
@userid char(4),
@passwd char(4),
@loginmessage char(20)OUT
AS  
IF EXISTS	 (SELECT top 1 * FROM appuser where userid = @userid
  AND RTRIM(passwd)  = RTRIM(@passwd)) 
  SELECT @loginmessage = 'OK' 
else
  SELECT @loginmessage = 'Login Failed' 

 
GO

IF OBJECT_ID('dbo.sp_checkuserrole') IS NOT NULL
  DROP PROCEDURE dbo.sp_checkuserrole
  GO
CREATE PROCEDURE sp_checkuserrole

@userid char(4),
@requiredrole char(4),
@rolemessage char(20) OUT
AS
DECLARE @userrole char(4) = ''
SELECT @userrole = (SELECT top 1 userrole FROM appuser where userid = @userid)
-- Allow all processes for admininstrators
IF @userrole= 'SYAD' OR @userid = 'TRAD'
  BEGIN
    SELECT @rolemessage = 'OK'  
  END
ELSE
  BEGIN
    IF @userrole = @requiredrole
      SELECT @rolemessage = 'OK'  
    ELSE
      SELECT @rolemessage  = 'Not Authorized' 
  END
GO

 
       