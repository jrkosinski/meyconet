USE meycosqldata
IF OBJECT_ID('dbo.sp_marktrackingeventmailsent') IS NOT NULL
  DROP PROCEDURE dbo.sp_marktrackingeventmailsent
  GO
CREATE PROCEDURE sp_marktrackingeventmailsent
@idcol int,
@userid char(4)
AS  
  -- Set sentmail to 1
  UPDATE jtrak SET sentmail = 1 , lckuser = @userid, lckdate = GETDATE()

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


SELECT b.code, b.descrip, a.route, a.stepid FROM route a
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

IF OBJECT_ID('dbo.wsgsp_getbatchinvoices') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchinvoices
  GO
CREATE PROCEDURE wsgsp_getbatchinvoices
AS

select sono, b.location, b.code, b.idcol
 from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printinvoice = 'Y'
ORDER BY sono
GO

IF OBJECT_ID('dbo.wsgsp_getbatchworkorders') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchworkorders
  GO
CREATE PROCEDURE wsgsp_getbatchworkorders
AS

select sono, b.location, b.code, b.idcol from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printworkorder = 'Y'
ORDER BY sono
GO

IF OBJECT_ID('dbo.wsgsp_getbatchpacklists') IS NOT NULL
  DROP PROCEDURE wsgsp_getbatchpacklists
  GO
CREATE PROCEDURE wsgsp_getbatchpacklists
AS

select sono, b.location, b.code, b.idcol from (select SoNo, sentmail, stepid, idcol, upstrack, seq, trackdate, 
row_number() over (partition by sono  order by adddate DESC )  as RowNum from jtrak  WHERE cancelled <> 1) X
INNER JOIN step b
ON X.stepid = b.idcol
where RowNum = 1 AND b.printpacklist = 'Y'
ORDER BY sono
GO
