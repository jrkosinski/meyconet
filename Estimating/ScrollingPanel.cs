using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using WSGUtilitieslib;

namespace Estimating
{
    public class ScrollingPanel
    {
        protected int _maxY = 0;
        protected System.Windows.Forms.Panel _panel;
        protected List<Panel> _subPanels = new List<Panel>();
        protected System.Windows.Forms.Form _parentForm;

        public int Count
        {
            get { return this._subPanels.Count; }
        }

        public bool Enabled
        {
            get { return this._panel.Enabled; }
            set
            {
                this._panel.Enabled = value;
                for (int n = 0; n < this._subPanels.Count; n++)
                {
                    this._subPanels[n].Enabled = value;
                    foreach (Control ctl in this._subPanels[n].Controls)
                    {
                        ctl.Enabled = value;
                    }
                }
            }
        }

        public ScrollingPanel(Form parentForm, Panel panel)
        {
            this._panel = panel;
            this._parentForm = parentForm;
            this._panel.Enabled = true;
        }


        public virtual void Clear()
        {
            this._panel.Controls.Clear();
            _maxY = 0;
        }

        public void AddSubPanel(System.Windows.Forms.Panel subPanel)
        {
            this._parentForm.Invoke((MethodInvoker)delegate
            {
                subPanel.Location = new Point(0, this._maxY);
                this._maxY += subPanel.Height;
                this._panel.Controls.Add(subPanel);
                this._subPanels.Add(subPanel);
            });
        }
    }
}// namespace