using HostTest.Util;
using ServicoDeOperacaoClienteUsuarios;
using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace HostTest
{
    public partial class Form1 : Form
    {
        private string status = "started";
        private ServiceHost vHost;
  
        public Form1()
        {
            InitializeComponent();
            starStopService(1);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if("stopped".Equals(status)){
                starStopService(1);                
                status = "started";
                btnStartStop.Text = "Stop service";
                lbStatus.Text = "Service started successfully.";                
            }
            else
            {
                starStopService(0);
                status = "stopped";
                btnStartStop.Text = "Start service";
                lbStatus.Text = "Service stopped successfully.";                
            }
        }

        void starStopService(int i) 
        {
            try
            {
                if(i == 1)
                {
                    vHost = new ServiceHost(typeof(Service));

                    NetTcpBinding b = new NetTcpBinding();
                    b.MaxReceivedMessageSize = 65536 * 20;
                    b.Security.Mode = SecurityMode.None;
                    string a = FilesINI.ReadValue("Host", "addressServer");
                    vHost.AddServiceEndpoint(typeof(IService), b, new Uri(a));
                    vHost.Open();
                }
                else if(i == 0)
                {
                    vHost.Abort();
                    vHost.Close();
                }
                else
                {
                    lbStatus.Text = "Error this operations.";
                }
            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
            }
        }
    }
}
