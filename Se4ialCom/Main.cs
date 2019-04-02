using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Configuration;

namespace Se4ialCom
{
    public partial class Main : Form
    {
        string RcvData1, RcvData2, RcvData3, RcvData4;
        string CrgReturn = "\r\n";
        bool Formload = true;
        int intTBREST1, intTBREST2, intTBREST3, intTBREST4 = 1;
        int Port1_Send, Port2_Send, Port3_Send, Port4_Send, Port1_Recieved, Port2_Recieved, Port3_Recieved, Port4_Recieved = 0;
        private delegate void focusDelegate(Control strText);
        ArrayList xx = new ArrayList();
        public Main()
        {
            try
            {
                InitializeComponent();
                String[] PortNames = SerialPort.GetPortNames();
                if (PortNames != null)
                {
                    for (int i = 0; i < PortNames.Length; i++)
                    {
                        cbCOMPORT1.Items.Add(PortNames[i]);
                        cbCOMPORT2.Items.Add(PortNames[i]);
                        cbCOMPORT3.Items.Add(PortNames[i]);
                        cbCOMPORT4.Items.Add(PortNames[i]);
                    }
                    cbCOMPORT1.SelectedIndex = 0;
                    cbCOMPORT2.SelectedIndex = 0;
                    cbCOMPORT3.SelectedIndex = 0;
                    cbCOMPORT4.SelectedIndex = 0;

                }
                btCLOSE1.Enabled = false;
                btCLOSE2.Enabled = false;
                btCLOSE3.Enabled = false;
                btCLOSE4.Enabled = false;
            }
            catch (Exception ex)
            {
                for (int i = 1; i < 31; i++)
                {
                    cbCOMPORT1.Items.Add("COM" + i);
                    cbCOMPORT2.Items.Add("COM" + i);
                    cbCOMPORT3.Items.Add("COM" + i);
                    cbCOMPORT4.Items.Add("COM" + i);
                }
                cbCOMPORT1.SelectedIndex = 0;
                cbCOMPORT2.SelectedIndex = 0;
                cbCOMPORT3.SelectedIndex = 0;
                cbCOMPORT4.SelectedIndex = 0;
                
                btCLOSE1.Enabled = false;
                btCLOSE2.Enabled = false;
                btCLOSE3.Enabled = false;
                btCLOSE4.Enabled = false;

                MessageBox.Show("현재 사용할 수 있는 SerialPort가 없습니다.");

            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (Formload == false)
                Application.Exit();
        }
        //========================================================================================
        // 접속 및 종료시 사용권한
        //========================================================================================
        public void OPEN_ENABLE(ComboBox com, ComboBox bitrate, ComboBox databit, ComboBox parity, ComboBox stop, GroupBox group)
        {
            if (group.Text == "Port 1")
            {
                Port1_Recieved = 0;
                Port1_Send = 0;
            }
            else if (group.Text == "Port 2")
            {
                Port2_Recieved = 0;
                Port2_Send = 0;
            }
            else if (group.Text == "Port 3")
            {
                Port3_Recieved = 0;
                Port3_Send = 0;
            }
            else if (group.Text == "Port 4")
            {
                Port4_Recieved = 0;
                Port4_Send = 0;
            }

            com.Enabled = false;
            bitrate.Enabled = false;
            databit.Enabled = false;
            parity.Enabled = false;
            stop.Enabled = false;
        }
        public void CLOSE_ENABLE(ComboBox com, ComboBox bitrate, ComboBox databit, ComboBox parity, ComboBox stop, GroupBox group)
        {

            if (group.Text == "Port 1")
            {
                Port1_Recieved = 0;
                Port1_Send = 0;
            }
            else if (group.Text == "Port 2")
            {
                Port2_Recieved = 0;
                Port2_Send = 0;
            }
            else if (group.Text == "Port 3")
            {
                Port3_Recieved = 0;
                Port3_Send = 0;
            }
            else if (group.Text == "Port 4")
            {
                Port4_Recieved = 0;
                Port4_Send = 0;
            }

            com.Enabled = true;
            bitrate.Enabled = true;
            databit.Enabled = true;
            parity.Enabled = true;
            stop.Enabled = true;
        }





        private void btOPEN1_Click(object sender, EventArgs e)
        {
            try
            {
                SP1.PortName = cbCOMPORT1.Text;
                SP1.BaudRate = Convert.ToInt32(cbBITRATE1.Text);
                SP1.DataBits = Convert.ToInt32(cbDATEBIT1.Text);
                SP1.Parity = (Parity)Enum.Parse(typeof(Parity), cbPARITY1.Text);
                SP1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbSTOPBIT1.Text);
                SP1.ReadTimeout = (int)500;
                SP1.WriteTimeout = (int)500;
                SP1.Open();
                //MessageBox.Show(cbCOMPORT1.Text + "에 접속하였습니다.");
                OPEN_ENABLE(cbBITRATE1, cbCOMPORT1, cbDATEBIT1, cbPARITY1, cbSTOPBIT1, groupBox1);
                btOPEN1.Enabled = false;
                btCLOSE1.Enabled = true;

            }
            catch
            {
                MessageBox.Show(cbCOMPORT1.Text + "접속에 실패하였습니다.");
                btOPEN1.Enabled = true;
                btCLOSE1.Enabled = false;
            }
        }

        private void btOPEN2_Click(object sender, EventArgs e)
        {
            try
            {
                SP2.PortName = cbCOMPORT2.Text;
                SP2.BaudRate = Convert.ToInt32(cbBITRATE2.Text);
                SP2.DataBits = Convert.ToInt32(cbDATEBIT2.Text);
                SP2.Parity = (Parity)Enum.Parse(typeof(Parity), cbPARITY2.Text);
                SP2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbSTOPBIT2.Text);
                SP2.ReadTimeout = (int)500;
                SP2.WriteTimeout = (int)500;
                SP2.Open();
                //MessageBox.Show(cbCOMPORT2.Text + "에 접속하였습니다.");
                OPEN_ENABLE(cbBITRATE2, cbCOMPORT2, cbDATEBIT2, cbPARITY2, cbSTOPBIT2, groupBox2);
                btOPEN2.Enabled = false;
                btCLOSE2.Enabled = true;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT2.Text + "접속에 실패하였습니다.");
                btOPEN2.Enabled = true;
                btCLOSE2.Enabled = false;
            }
        }

        private void btOPEN3_Click(object sender, EventArgs e)
        {
            try
            {
                SP3.PortName = cbCOMPORT3.Text;
                SP3.BaudRate = Convert.ToInt32(cbBITRATE3.Text);
                SP3.DataBits = Convert.ToInt32(cbDATEBIT3.Text);
                SP3.Parity = (Parity)Enum.Parse(typeof(Parity), cbPARITY3.Text);
                SP3.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbSTOPBIT3.Text);
                SP3.ReadTimeout = (int)500;
                SP3.WriteTimeout = (int)500;
                SP3.Open();
                //MessageBox.Show(cbCOMPORT3.Text + "에 접속하였습니다.");
                OPEN_ENABLE(cbBITRATE3, cbCOMPORT3, cbDATEBIT3, cbPARITY3, cbSTOPBIT3, groupBox3);
                btOPEN3.Enabled = false;
                btCLOSE3.Enabled = true;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT3.Text + "접속에 실패하였습니다.");
                btOPEN3.Enabled = true;
                btCLOSE3.Enabled = false;
            }
        }

        private void btOPEN4_Click(object sender, EventArgs e)
        {
            try
            {
                SP4.PortName = cbCOMPORT4.Text;
                SP4.BaudRate = Convert.ToInt32(cbBITRATE4.Text);
                SP4.DataBits = Convert.ToInt32(cbDATEBIT4.Text);
                SP4.Parity = (Parity)Enum.Parse(typeof(Parity), cbPARITY4.Text);
                SP4.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbSTOPBIT4.Text);
                SP4.ReadTimeout = (int)500;
                SP4.WriteTimeout = (int)500;
                SP4.Open();
                //MessageBox.Show(cbCOMPORT4.Text + "에 접속하였습니다.");
                OPEN_ENABLE(cbBITRATE4, cbCOMPORT4, cbDATEBIT4, cbPARITY4, cbSTOPBIT4, groupBox4);
                btOPEN4.Enabled = false;
                btCLOSE4.Enabled = true;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT4.Text + "접속에 실패하였습니다.");
                btOPEN4.Enabled = true;
                btCLOSE4.Enabled = false;
            }
        }

        private void btCLOSE1_Click(object sender, EventArgs e)
        {
            try
            {
                SP1.Close();
                intTBREST1 = 1;
                CLOSE_ENABLE(cbBITRATE1, cbCOMPORT1, cbDATEBIT1, cbPARITY1, cbSTOPBIT1, groupBox1);
                btOPEN1.Enabled = true;
                btCLOSE1.Enabled = false;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT1.Text + "접속종료에 실패하였습니다.");
                btOPEN1.Enabled = false;
                btCLOSE1.Enabled = true;
            }
        }

        private void btCLOSE2_Click(object sender, EventArgs e)
        {
            try
            {
                SP2.Close();
                intTBREST2 = 1;
                CLOSE_ENABLE(cbBITRATE2, cbCOMPORT2, cbDATEBIT2, cbPARITY2, cbSTOPBIT2, groupBox2);
                btOPEN2.Enabled = true;
                btCLOSE2.Enabled = false;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT2.Text + "접속종료에 실패하였습니다.");
                btOPEN2.Enabled = false;
                btCLOSE2.Enabled = true;
            }
        }

        private void btCLOSE3_Click(object sender, EventArgs e)
        {
            try
            {
                SP3.Close();
                intTBREST3 = 1;
                CLOSE_ENABLE(cbBITRATE3, cbCOMPORT3, cbDATEBIT3, cbPARITY3, cbSTOPBIT3, groupBox3);
                btOPEN3.Enabled = true;
                btCLOSE3.Enabled = false;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT3.Text + "접속종료에 실패하였습니다.");
                btOPEN3.Enabled = false;
                btCLOSE3.Enabled = true;
            }
        }

        private void btCLOSE4_Click(object sender, EventArgs e)
        {
            try
            {
                SP4.Close();
                intTBREST4 = 1;
                CLOSE_ENABLE(cbBITRATE4, cbCOMPORT4, cbDATEBIT4, cbPARITY4, cbSTOPBIT4, groupBox4);
                btOPEN4.Enabled = true;
                btCLOSE4.Enabled = false;
            }
            catch
            {
                MessageBox.Show(cbCOMPORT4.Text + "접속종료에 실패하였습니다.");
                btOPEN4.Enabled = false;
                btCLOSE4.Enabled = true;
            }
        }

        private void btSEND1_Click(object sender, EventArgs e)
        {
            SendingMessage(SP1, btOPEN1, cbTXHEX1, tbSEND1, groupBox1, chbPORT1);
        }

        private void btSEND1_1_Click(object sender, EventArgs e)
        {
            SendingMessage(SP1, btOPEN1, cbTXHEX1_1, tbSEND1_1, groupBox1, chbPORT1_1);
        }

        private void btSEND2_Click(object sender, EventArgs e)
        {
            SendingMessage(SP2, btOPEN2, cbTXHEX2, tbSEND2, groupBox2, chbPORT2);
        }

        private void btSEND2_1_Click(object sender, EventArgs e)
        {
            SendingMessage(SP2, btOPEN2, cbTXHEX2_1, tbSEND2_1, groupBox2, chbPORT2_1);
        }

        private void btSEND3_Click(object sender, EventArgs e)
        {
            SendingMessage(SP3, btOPEN3, cbTXHEX3, tbSEND3, groupBox3, chbPORT3);
        }

        private void btSEND3_1_Click(object sender, EventArgs e)
        {
            SendingMessage(SP3, btOPEN3, cbTXHEX3_1, tbSEND3_1, groupBox3, chbPORT3_1);
        }

        private void btSEND4_Click(object sender, EventArgs e)
        {
            SendingMessage(SP4, btOPEN4, cbTXHEX4, tbSEND4, groupBox4, chbPORT4);
        }

        private void btSEND4_1_Click(object sender, EventArgs e)
        {
            SendingMessage(SP4, btOPEN4, cbTXHEX4_1, tbSEND4_1, groupBox4, chbPORT4_1);
        }


        //========================================================================================
        // 데이터 수신 부분
        //========================================================================================
        public void RecievdDATA(string RcvData, SerialPort SP, ComboBox cbRXHEX, TextBox tbPORT, GroupBox groupboxes)
        {
            try
            {
                RcvData = SP.ReadExisting(); // 시리얼에서 들어온 데이터를 집어넣는다.

                if (cbRXHEX.Text == "ASCII")
                {
                    RcvData = RcvData.Replace("\r", "");
                    RcvData = RcvData.Replace("\n", "\r\n");
                    tbPORT.AppendText(RcvData);
                    tbPORT.ScrollToCaret();


                    if (groupboxes.Text == "Port 1")
                    {
                        Port1_Recieved += RcvData.Length;
                        lb_Port1_rcv.Text = Port1_Recieved.ToString();

                        if (Port1_Recieved >= 32760 * intTBREST1 && intTBREST1 != 0)
                        {
                            intTBREST1++;
                            tbPORT1.Text = "";
                        }
                    }
                    else if (groupboxes.Text == "Port 2")
                    {
                        Port2_Recieved += RcvData.Length;
                        lb_Port2_rcv.Text = Port2_Recieved.ToString();

                        if (Port2_Recieved >= 32760 * intTBREST2 && intTBREST2 != 0)
                        {
                            intTBREST2++;
                            tbPORT2.Text = "";
                        }
                    }
                    else if (groupboxes.Text == "Port 3")
                    {
                        Port3_Recieved += RcvData.Length;
                        lb_Port3_rcv.Text = Port3_Recieved.ToString();

                        if (Port3_Recieved >= 32760 * intTBREST3 && intTBREST3 != 0)
                        {
                            intTBREST3++;
                            tbPORT3.Text = "";
                        }
                    }
                    else if (groupboxes.Text == "Port 4")
                    {
                        Port4_Recieved += RcvData.Length;
                        lb_Port4_rcv.Text = Port4_Recieved.ToString();

                        if (Port4_Recieved >= 32760 * intTBREST4 && intTBREST4 != 0)
                        {
                            intTBREST4++;
                            tbPORT4.Text = "";
                        }
                    }


                    Update();
                    Application.DoEvents();

                }
                else if (cbRXHEX.Text == "HEX")
                {
                    RcvData = RcvData.Replace("\r", "");
                    RcvData = RcvData.Replace("\n", "\r\n");
                    tbPORT.AppendText(ConvertToHex(RcvData));
                    tbPORT.ScrollToCaret();


                    if (groupboxes.Text == "Port 1")
                    {
                        Port1_Recieved += RcvData.Length;
                        lb_Port1_rcv.Text = Port1_Recieved.ToString();

                        if (Port1_Recieved >= 32760 * intTBREST1 && intTBREST1 != 0)
                        {
                            intTBREST1++;
                            tbPORT1.Text = "";
                        }
                    }
                    else if (groupboxes.Text == "Port 2")
                    {
                        Port2_Recieved += RcvData.Length;
                        lb_Port2_rcv.Text = Port2_Recieved.ToString();

                        if (Port2_Recieved >= 32760 * intTBREST2 && intTBREST2 != 0)
                        {
                            intTBREST2++;
                            tbPORT2.Text = "";
                        }
                    }
                    else if (groupboxes.Text == "Port 3")
                    {
                        Port3_Recieved += RcvData.Length;
                        lb_Port3_rcv.Text = Port3_Recieved.ToString();

                        if (Port3_Recieved >= 32760 * intTBREST3 && intTBREST3 != 0)
                        {
                            intTBREST3++;
                            tbPORT3.Text = "";
                        }
                    }
                    else if (groupboxes.Text == "Port 4")
                    {
                        Port4_Recieved += RcvData.Length;
                        lb_Port4_rcv.Text = Port4_Recieved.ToString();

                        if (Port4_Recieved >= 32760 * intTBREST4 && intTBREST4 != 0)
                        {
                            intTBREST4++;
                            tbPORT4.Text = "";
                        }
                    }


                    Update();
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }

        }


        private void SP1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SP1.BytesToRead > 0)
            {
                this.Invoke(new MethodInvoker(delegate
                       {
                           RecievdDATA(RcvData1, SP1, cbRXHEX1, tbPORT1, groupBox1);
                       }));
            }

        }

        private void SP2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SP2.BytesToRead > 0)
            {
                this.Invoke(new MethodInvoker(delegate
                       {
                           RecievdDATA(RcvData2, SP2, cbRXHEX2, tbPORT2, groupBox2);
                       }));
            }
        }

        private void SP3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SP3.BytesToRead > 0)
            {
                this.Invoke(new MethodInvoker(delegate
                       {
                           RecievdDATA(RcvData3, SP3, cbRXHEX3, tbPORT3, groupBox3);
                       }));
            }
        }

        private void SP4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SP4.BytesToRead > 0)
            {
                this.Invoke(new MethodInvoker(delegate
                       {
                           RecievdDATA(RcvData4, SP4, cbRXHEX4, tbPORT4, groupBox4);
                       }));
            }


        }


        //========================================================================================
        // 메세지 전송부분
        //========================================================================================
        public void SendingMessage(SerialPort Serial, Button btOPEN, ComboBox chTXHEX, TextBox tbSend, GroupBox groupboxes, CheckBox chbPORT)
        {
            if (btOPEN.Enabled == true)
            {
                return;
            }
            try
            {
                if (chTXHEX.Text == "ASCII")
                {
                    if (chbPORT.Checked)
                    {
                        string Send = tbSend.Text + "\r\n";
                        Serial.Write(Encoding.Default.GetBytes(Send), 0, Send.Length);


                        if (groupboxes.Text == "Port 1")
                        {
                            Port1_Send += Send.Length;
                            lb_Port1_send.Text = Port1_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 2")
                        {
                            Port2_Send += Send.Length;
                            lb_Port2_send.Text = Port2_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 3")
                        {
                            Port3_Send += Send.Length;
                            lb_Port3_send.Text = Port3_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 4")
                        {
                            Port4_Send += Send.Length;
                            lb_Port4_send.Text = Port4_Send.ToString();
                        }
                    }
                    else if (!chbPORT.Checked)
                    {
                        string Send = tbSend.Text;
                        Serial.Write(Encoding.Default.GetBytes(Send), 0, Send.Length);


                        if (groupboxes.Text == "Port 1")
                        {
                            Port1_Send += Send.Length;
                            lb_Port1_send.Text = Port1_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 2")
                        {
                            Port2_Send += Send.Length;
                            lb_Port2_send.Text = Port2_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 3")
                        {
                            Port3_Send += Send.Length;
                            lb_Port3_send.Text = Port3_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 4")
                        {
                            Port4_Send += Send.Length;
                            lb_Port4_send.Text = Port4_Send.ToString();
                        }
                    }

                }
                else if (chTXHEX.Text == "HEX")
                {

                    if (chbPORT.Checked)
                    {
                        string Send = ConvertToAscii(tbSend.Text) + "\r\n";
                        Serial.Write(Encoding.Default.GetBytes(Send), 0, Send.Length);

                        if (groupboxes.Text == "Port 1")
                        {
                            Port1_Send += Send.Length;
                            lb_Port1_send.Text = Port1_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 2")
                        {
                            Port2_Send += Send.Length;
                            lb_Port2_send.Text = Port2_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 3")
                        {
                            Port3_Send += Send.Length;
                            lb_Port3_send.Text = Port3_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 4")
                        {
                            Port4_Send += Send.Length;
                            lb_Port4_send.Text = Port4_Send.ToString();
                        }
                    }
                    else if (!chbPORT.Checked)
                    {
                        string Send = ConvertToAscii(tbSend.Text);
                        Serial.Write(Encoding.Default.GetBytes(Send), 0, Send.Length);

                        if (groupboxes.Text == "Port 1")
                        {
                            Port1_Send += Send.Length;
                            lb_Port1_send.Text = Port1_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 2")
                        {
                            Port2_Send += Send.Length;
                            lb_Port2_send.Text = Port2_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 3")
                        {
                            Port3_Send += Send.Length;
                            lb_Port3_send.Text = Port3_Send.ToString();
                        }
                        else if (groupboxes.Text == "Port 4")
                        {
                            Port4_Send += Send.Length;
                            lb_Port4_send.Text = Port4_Send.ToString();
                        }
                    }

                }
            }
            catch
            {
                if (groupboxes.Text == "Port 1")
                {
                    tbINTERVAL1.Enabled = true;
                    timer1.Stop();
                    btAUTO1.Text = "Repeat";
                }
                else if (groupboxes.Text == "Port 2")
                {
                    tbINTERVAL2.Enabled = true;
                    timer2.Stop();
                    btAUTO2.Text = "Repeat";
                }
                else if (groupboxes.Text == "Port 3")
                {
                    tbINTERVAL3.Enabled = true;
                    timer3.Stop();
                    btAUTO3.Text = "Repeat";
                }
                else if (groupboxes.Text == "Port 4")
                {
                    tbINTERVAL4.Enabled = true;
                    timer4.Stop();
                    btAUTO4.Text = "Repeat";
                }
                MessageBox.Show("메세지전송을 실패하였습니다!");
            }
        }
        //========================================================================================
        // 반복 메세지 전송부분
        //========================================================================================
        public void AutoSendingMessage1(object sender, EventArgs e)
        {
            if (rbAUTO1.Checked)
                SendingMessage(SP1, btOPEN1, cbTXHEX1, tbSEND1, groupBox1, chbPORT1);
            else if (rbAUTO1_1.Checked)
                SendingMessage(SP1, btOPEN1, cbTXHEX1_1, tbSEND1_1, groupBox1, chbPORT1_1);
        }
        public void AutoSendingMessage2(object sender, EventArgs e)
        {
            if (rbAUTO2.Checked)
                SendingMessage(SP2, btOPEN2, cbTXHEX2, tbSEND2, groupBox2, chbPORT2);
            else if (rbAUTO2_1.Checked)
                SendingMessage(SP2, btOPEN2, cbTXHEX2_1, tbSEND2_1, groupBox2, chbPORT2_1);
        }
        public void AutoSendingMessage3(object sender, EventArgs e)
        {
            if (rbAUTO3.Checked)
                SendingMessage(SP3, btOPEN3, cbTXHEX3, tbSEND3, groupBox3, chbPORT3);
            else if (rbAUTO3_1.Checked)
                SendingMessage(SP3, btOPEN3, cbTXHEX3_1, tbSEND3_1, groupBox3, chbPORT3_1);
        }
        public void AutoSendingMessage4(object sender, EventArgs e)
        {
            if (rbAUTO4.Checked)
                SendingMessage(SP4, btOPEN4, cbTXHEX4, tbSEND4, groupBox4, chbPORT4);
            else if (rbAUTO4_1.Checked)
                SendingMessage(SP4, btOPEN4, cbTXHEX4_1, tbSEND4_1, groupBox4, chbPORT4_1);
        }


        //========================================================================================
        // 반복 전송 부분
        //========================================================================================

        private void btAUTO1_Click(object sender, EventArgs e)
        {
            try
            {
                if (btOPEN1.Enabled == true)
                {
                    return;
                }
                else if (btOPEN1.Enabled == false)
                {
                    if (btAUTO1.Text == "Repeat")
                    {
                        timer1.Interval = Convert.ToInt32(tbINTERVAL1.Text);
                        tbINTERVAL1.Enabled = false;
                        timer1.Start();
                        btAUTO1.Text = "Stop";
                    }
                    else if (btAUTO1.Text == "Stop")
                    {
                        tbINTERVAL1.Enabled = true;
                        timer1.Stop();
                        btAUTO1.Text = "Repeat";
                    }
                }
            }
            catch
            {
                MessageBox.Show("시간에는 1이상의 자연수만 입력해야 합니다.");
            }
        }

        private void btAUTO2_Click(object sender, EventArgs e)
        {
            try
            {
                if (btOPEN2.Enabled == true)
                {
                    return;
                }
                else if (btOPEN2.Enabled == false)
                {
                    if (btAUTO2.Text == "Repeat")
                    {
                        timer2.Interval = Convert.ToInt32(tbINTERVAL2.Text);
                        tbINTERVAL2.Enabled = false;
                        timer2.Start();
                        btAUTO2.Text = "Stop";
                    }
                    else if (btAUTO2.Text == "Stop")
                    {
                        tbINTERVAL2.Enabled = true;
                        timer2.Stop();
                        btAUTO2.Text = "Repeat";
                    }
                }
            }
            catch
            {
                MessageBox.Show("시간에는 1이상의 자연수만 입력해야 합니다.");
            }
        }

        private void btAUTO3_Click(object sender, EventArgs e)
        {
            try
            {
                if (btOPEN3.Enabled == true)
                {
                    return;
                }
                else if (btOPEN3.Enabled == false)
                {
                    if (btAUTO3.Text == "Repeat")
                    {
                        timer3.Interval = Convert.ToInt32(tbINTERVAL3.Text);
                        tbINTERVAL3.Enabled = false;
                        timer3.Start();
                        btAUTO3.Text = "Stop";
                    }
                    else if (btAUTO3.Text == "Stop")
                    {
                        tbINTERVAL3.Enabled = true;
                        timer3.Stop();
                        btAUTO3.Text = "Repeat";
                    }
                }
            }
            catch
            {
                MessageBox.Show("시간에는 1이상의 자연수만 입력해야 합니다.");
            }
        }

        private void btAUTO4_Click(object sender, EventArgs e)
        {
            try
            {
                if (btOPEN4.Enabled == true)
                {
                    return;
                }
                else if (btOPEN4.Enabled == false)
                {
                    if (btAUTO4.Text == "Repeat")
                    {
                        timer4.Interval = Convert.ToInt32(tbINTERVAL4.Text);
                        tbINTERVAL4.Enabled = false;
                        timer4.Start();
                        btAUTO4.Text = "Stop";
                    }
                    else if (btAUTO4.Text == "Stop")
                    {
                        tbINTERVAL4.Enabled = true;
                        timer4.Stop();
                        btAUTO4.Text = "Repeat";
                    }
                }
            }
            catch
            {
                MessageBox.Show("시간에는 1이상의 자연수만 입력해야 합니다.");
            }
        }




























        //========================================================================================
        // Hex코드 -> 아스키코드
        //========================================================================================
        public string ConvertToAscii(string HexaString)
        {
            string StrValue = "";
            while (HexaString.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexaString.Substring(0, 2), 16)).ToString();
                HexaString = HexaString.Substring(2, HexaString.Length - 2);
            }
            //            StrValue = StrValue.Replace("0d 0a", "\n");
            return StrValue;
        }

        //========================================================================================
        // 아스키코드 -> Hex코드
        //========================================================================================
        public string ConvertToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2} ", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }






        ////========================================================================================
        //// Hex코드 -> 아스키코드
        ////========================================================================================

        //private string HexToString(string str)
        //{
        //    char[] token = { ' ', '\r', '\n' };

        //    string[] strArray = str.Split(token);


        //    string temp = "";

        //    int dec = 0;

        //    for (int i = 0; i < strArray.Length; i++)
        //    {

        //        dec = Convert.ToInt32(strArray[i], 16);

        //        temp += string.Format("{0}", (char)dec);
        //    }
        //    return temp;  

        //}


        ////========================================================================================
        //// 아스키코드 -> Hex코드
        ////========================================================================================

        //private string StringToHex(string str)
        //{
        //    char[] ch = str.ToCharArray();

        //    string temp = "";

        //    for (int i = 0; i < ch.Length; i++)
        //        temp += String.Format("{0:X2}", (int)ch[i]) + " ";

        //    return temp;
        //}

        private void btCLEAR1_Click(object sender, EventArgs e)
        {
            tbPORT1.Text = "";
        }

        private void btCLEAR2_Click(object sender, EventArgs e)
        {
            tbPORT2.Text = "";
        }

        private void btCLEAR3_Click(object sender, EventArgs e)
        {
            tbPORT3.Text = "";
        }

        private void btCLEAR4_Click(object sender, EventArgs e)
        {
            tbPORT4.Text = "";
        }

        private void Main_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("만든이 : 김남원 \n연락처 : knw1234@gmail.com\t ");
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbPORT1.Text = "";
            tbPORT2.Text = "";
            tbPORT3.Text = "";
            tbPORT4.Text = "";

        }












    }
}
