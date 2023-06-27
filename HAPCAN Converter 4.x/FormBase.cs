using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCAN_Converter;

public partial class FormBase : Form
{
    public FormBase()
    {
        //resizing form
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        InitializeComponent();
    }
    //moving form
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

    private void panelTop_MouseDown(object sender, MouseEventArgs e)
    {
        ReleaseCapture();
        SendMessage(this.Handle, 0x112, 0xf012, 0);
    }

    //resizing form
    int Mx;
    int My;
    int Fw;
    int Fh;
    bool moving;
    private void picResizer_MouseDown(object sender, MouseEventArgs e)
    {
        moving = true;
        My = MousePosition.Y;
        Mx = MousePosition.X;
        Fw = this.Width;
        Fh = this.Height;
    }
    private void picResizer_MouseMove(object sender, MouseEventArgs e)
    {
        if (moving)
        {
            this.Width = MousePosition.X - Mx + Fw;
            this.Height = MousePosition.Y - My + Fh;
        }
    }
    private void picResizer_MouseUp(object sender, MouseEventArgs e)
    {
        moving = false;
    }

    //minimizing form
    private void btnMin_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }
    //minimizes window when clicking on taskbar
    const int WS_MINIMIZEBOX = 0x20000;
    const int CS_DBLCLKS = 0x8;
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style |= WS_MINIMIZEBOX;
            cp.ClassStyle |= CS_DBLCLKS;
            return cp;
        }
    }

    //maximizing form
    int Fx;
    int Fy;
    private void btnMax_Click(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Normal)
        {
            //keep form location
            Fx = this.Left;
            Fy = this.Top;
            //make sure it doesn't cover windows start menu and starts from 0,0 on a screen
            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            this.MaximizedBounds = workingArea;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
        }
        else
        {
            //restore form location
            this.Left = Fx;
            this.Top = Fy;
            this.WindowState = FormWindowState.Normal;
        }
    }

    //close
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
