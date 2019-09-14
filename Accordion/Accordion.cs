﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Streampanel
{
    public partial class Accordion : Panel
    {
        public Accordion()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        public void Add(Expander expander)
        {
            if (this.Controls.Count > 0)
                expander.Collapse();

            expander.Width = this.Width - this.Margin.Horizontal - expander.Margin.Horizontal;
            this.Controls.Add(expander);

            expander.StateChanging += new CancelEventHandler(expander_StateChanging);
            expander.StateChanged += new EventHandler(expander_StateChanged);

            ArrangeLayout();
        }

        void expander_StateChanging(object sender, CancelEventArgs e)
        {
            if (this.processing)
                return;

            /*Expander expander = sender as Expander;
            if (expander.Expanded)
                e.Cancel = true;*/
        }

        void expander_StateChanged(object sender, EventArgs e)
        {
            if (this.processing)
                return;

            processing = true;
            Expander expander = sender as Expander;
            foreach (Expander ex in Controls)
            {
                if (ex == expander)
                    continue;

                /*if (ex.Expanded)
                    ex.Collapse();*/
            }

            ArrangeLayout();

            processing = false;
        }

        private void ArrangeLayout()
        {
            int h = 0;
            int y = this.Padding.Top;
            foreach (Expander ex in this.Controls)
            {
                ex.Width = this.Width;
                ex.Top = y;
                if (ex.Expanded)
                {
                    ex.Height = ex.Header.Height + ex.Content.Height;
                }
                h += ex.Height;

                y += ex.Height;
            }

            this.Size = new Size(210, h);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ArrangeLayout();
        }

        private bool processing = false;
    }
}
