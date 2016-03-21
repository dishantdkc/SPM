using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfFileWriter;

namespace Sales
{
    public partial class Form1 : Form
    {

        private DataBase database= new DataBase();


        private GroupBox[] groupbox = new GroupBox[5];
        private Panel[] panel = new Panel[5];
        private Label[] label = new Label[15000];
        private int LabelCount = 0;
        private System.Windows.Forms.TextBox[] textbox = new System.Windows.Forms.TextBox[10];
        private ComboBox[] combobox = new ComboBox[5];
        private Button[] button = new Button[5];
        private NumericUpDown[] numericupdown = new NumericUpDown[5];
        private Point point = new Point(0, 0);
        private Font font12 = new Font("Microsoft Sans Serif", 12);
        private Font font10 = new Font("Microsoft Sans Serif", 10);
        private Font font8 = new Font("Microsoft Sans Serif", 8);
        private RadioButton[] radiobutton = new RadioButton[5];
        private ToolTip tooltip;// = new ToolTip();
        private DateTimePicker[] datepicker = new DateTimePicker[5];
        private int ToolTipPanelno = 0;

        private string name = "";    //     default user1
        private string password = "";//     default user1



        private int Panelcode;
        /*  panelcode
         * 1    home
         * 2    Sale
         * 3    Purchase
         * 4    Order
         * 5    Party
         * 6    Items
         * 7    Payment
         * 8    ActivityPanel...........
         */


        private int Pid = 0;
        

        private Point LoginPnlLoc = new Point(10, 60);
        private Point ControlPnlLoc = new Point(10, 120);
        private Point LoginTopLoc = new Point(10, 5);
        private Point ControlLoc = new Point(10, 135);
        private Point HomeButLoc = new Point(265, 265);

        //private SecondForm form2;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Main();
            
           // ShowSecondForm(1);

            


        }
        private void Main()
        {
            //database = new DataBase();

            Date.Text = DateTime.Today.Date.ToString();

            if (database.IfIsFirst())   // Check if Software started for first or Database refreshed
            {
                MessageBox.Show("Welcome user for first time,\n Your Defalut Login name is user1 and password is user1");

                if (!database.CreateNewDatabase())
                {
                    MessageBox.Show("Cant Create Database \n Contact Admin");

                }
                else
                {
                    database.AddTables();
                    database.SetDefaultCredentials();
                }
                //  create database
                //  Add Tables
                //  Set Default ID Pass

            }
            
            ShowLogin();
            
            


        }
        private void ShowLogin()
        {
            SetPanels();
            SetLoginPnlBig();
           

        }
        private void SetPanels()
        {
            Date.Text = DateTime.Today.ToShortDateString();
           
            {   // Hide All Panels
                HomePanel.Visible = false;
                LoginPanel.Visible = false;
                ItemPanel.Visible = false;
                PartyPanel.Visible = false;
                SalePanel.Visible = false;
                PurchasePanel.Visible = false;
                OrderPanel.Visible = false;
                PaymentPanel.Visible = false;
                ClearActivityPanel();
            }

            SetMainBox();
            
        }
        private void ClearActivityPanel()
        {
            ActivityPanel.Controls.Clear();
            ActivityPanel.Visible = false;
        }

        private void SetMainBox()
        {
            this.Width = 560;
            this.Height = 520;
            ShortCutText.Select();
        }
        private void SetLoginPnlSml()
        {
            ShortCutText.Enabled = true;

            LoginPanel.Visible = true;
            LoginPanel.Height = 70;
            LoginPanel.Width = 520;
            LoginPanel.Location = LoginPnlLoc;


            LoginTop.Visible = true;
            LoginTop.Width = 500;
            LoginTop.Height = 60;
            LoginTop.Location = LoginTopLoc;

            LoginSignin.Visible = false;
            Signinbox.Visible = false;
        }
        private void SetLoginPnlBig()
        {
            ShortCutText.Enabled = false;

            LoginPanel.Visible=true;
            LoginPanel.Height = 400;
            LoginPanel.Width = 520;
            LoginPanel.Location = LoginPnlLoc;


            LoginTop.Visible = true;
            LoginTop.Width = 500;
            LoginTop.Height = 60;
            LoginTop.Location = LoginTopLoc;

            LoginSignin.Visible = true;
            LoginSignin.Width = 500;
            LoginSignin.Height = 320;
            LoginSignin.Location = new Point(10, 70);

            Signinbox.Visible = true;
            Signinbox.Width = 400;
            Signinbox.Height = 200;
            Signinbox.Location = new Point(50, 50);

            LoginName.Select();
        }
        private void SetHome()
        {

            Panelcode = 1;
            SetPanels();
            SetLoginPnlSml();
            SetHomePnl();
            SetMainBox();
            CheckOrder();


        }
        private void SetHomePnl()
        {
            HomePanel.Visible = true;
            HomePanel.Width = 520;
            HomePanel.Height = 320;
            HomePanel.Location = ControlLoc;
        }
        private void SetItem()
        {

            Panelcode = 6;
            SetPanels();
            SetLoginPnlSml();
            SetItemPnl();
            SetMainBox();


        }
        private void SetPartyPnl()
        {
            PartyPanel.Visible = true;
            PartyPanel.Width = 520;
            PartyPanel.Height = 320;
            PartyPanel.Location = ControlLoc;
            PartyHome.Width = 80;
            PartyHome.Location = HomeButLoc;

        }
        private void SetParty()
        {
            Panelcode = 5;

            SetPanels();
            SetLoginPnlSml();
            SetPartyPnl();
            SetMainBox();

        }
        private void SetItemPnl()
        {

            ItemPanel.Visible = true;
            ItemPanel.Width = 520;
            ItemPanel.Height = 320;
            ItemPanel.Location = ControlLoc;
            ItemHome.Width = 80;
            ItemHome.Location = HomeButLoc;

        }
        private void SetSale()
        {
            Panelcode = 2;
            SetPanels();
            SetLoginPnlSml();
            SetSalePnl();
            SetMainBox();

        }
        private void SetSalePnl()
        {
            SalePanel.Visible = true;
            SalePanel.Width = 520;
            SalePanel.Height = 320;
            SalePanel.Location = ControlLoc;
            SaleHome.Location = HomeButLoc;
            SaleHome.Width = 80;

        }
        private void SetPurchase()
        {
            Panelcode = 3;
            SetPanels();
            SetLoginPnlSml();
            SetPurchasePnl();
            SetMainBox();

        }
        private void SetPurchasePnl()
        {
            PurchasePanel.Visible = true;
            PurchasePanel.Width = 520;
            PurchasePanel.Height = 320;
            PurchasePanel.Location = ControlLoc;
            PurchaseHome.Width = 80;
            PurchaseHome.Location = HomeButLoc;
        }
        private void SetOrder()
        {
            Panelcode = 4;
            SetPanels();
            SetLoginPnlSml();
            SetOrderPnl();
            SetMainBox();

        }
        private void SetOrderPnl()
        {
            OrderPanel.Visible = true;
            OrderPanel.Width = 520;
            OrderPanel.Height = 320;
            OrderPanel.Location = ControlLoc;
            OrderHome.Width = 80;
            OrderHome.Location = HomeButLoc;
        }
        private void SetPayment()
        {
            Panelcode = 7;
            SetPanels();
            SetLoginPnlSml();
            SetPaymentPnl();
            SetMainBox();
        }
        private void SetPaymentPnl()
        {
            PaymentPanel.Visible = true;
            PaymentPanel.Width = 520;
            PaymentPanel.Height = 320;
            PaymentPanel.Location = ControlLoc;
            PaymentHome.Width = 80;
            PaymentHome.Location = HomeButLoc;
        }
        
        
        
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (Panelcode < 7)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Exit Current Activity ");
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            Signin();

        }
        private void Signin()
        {
            try
            {
                name = LoginName.Text;
                password = LoginPass.Text;
            }
            catch
            {

            }
            if (name == "" || password == "")
            {
                MessageBox.Show("Please Enter Name and Password ", "Sign in ");
                LoginName.Select();
            }
            else
            {
                if (database.Login(name, password))
                {
                    SetHome();
                }
                else
                {
                    LoginName.Select();
                }
                
                    LoginName.Text = "";
                    LoginPass.Text = "";
                    LoginTopText.Text = database.LoginError;


            }


        } 


        private void SetActivityPanel(int choice)
        {



            switch (choice)
            {
                case 1:
                    {
                        //Heading.Text = "New Sale";
                        NewSale1();


                    }
                    break;
                case 2:
                    {
                        //Heading.Text = "View Sale";
                        ViewSale1();
                    }
                    break;
                case 3:
                    {
                        //Heading.Text = "Print Sale";
                        ViewSale2();
                    }
                    break;
                case 4:
                    {
                        //Heading.Text = "New Purchase";
                        NewPurchase1();
                    }
                    break;
                case 5:
                    {
                        //Heading.Text = "View Purchase";
                        ViewPurchase1();
                    }
                    break;
                case 6:
                    {
                        //Heading.Text = "Print Purchase";
                        ViewPurchase2();
                    }
                    break;
                case 7:
                    {
                        //Heading.Text = "New Order";
                        NewOrder1();
                    }
                    break;
                case 8:
                    {
                        //Heading.Text = "View Order";
                        OrderView1();
                    }
                    break;
                case 9:
                    {
                        //Heading.Text = "Edit Order";
                        // Excluded......................as not needed
                    }
                    break;
                case 10:
                    {
                        //Heading.Text = "New Party";
                        NewParty1();

                    }
                    break;
                case 11:
                    {
                        //Heading.Text = "View Party";
                        ViewPartys1();
                    }
                    break;
                case 12:
                    {
                        //Heading.Text = "Edit Party";
                        EditParty1();
                    }
                    break;
                case 13:
                    {
                        //Heading.Text = "New Item";
                        NewItem1();
                    }
                    break;
                case 14:
                    {
                        //Heading.Text = "View Item";
                        ViewItem1();
                    }
                    break;
                case 15:
                    {
                        //Heading.Text = "Edit Item";
                        EditItem1();
                    }
                    break;
                case 16:
                    {
                        // Payment Sale
                        PaySale1();
                    }
                    break;
                case 17:
                    {
                        // Payment Purchase
                        PayPurchase1();
                    }
                    break;
                case 18:
                    {
                            // Settings
                        Settings1();   
                    }
                    break;
            }

        }       //   Add function Calls here
        private void ShowActivityPannel(int choice)
        {
            SetPanels();
            SetLoginPnlSml();
            ActivityPanel.Visible = true;
            ActivityPanel.Location = ControlLoc;
            ActivityPanel.Width = 520;
            ActivityPanel.Height = 320;
            SetMainBox();
            Panelcode = 7;
            ShortCutText.Enabled = false;
            SetActivityPanel(choice);
        }


        private int InitLabel(int FontSize, int x, int y, int width, string text)
        {
            label[LabelCount] = new Label();

            switch (FontSize)
            {
                case 8:
                    {
                        label[LabelCount].Font = font8;

                    }
                    break;
                case 10:
                    {
                        label[LabelCount].Font = font10;

                    }
                    break;
                case 12:
                    {
                        label[LabelCount].Font = font12;
                    }
                    break;
                default:
                    {
                        label[LabelCount].Font = new Font("Microsoft Sans Serif", FontSize);
                    }
                    break;

            }



            point.X = x;
            point.Y = y;

            label[LabelCount].Location = point;

            label[LabelCount].Width = width;

            label[LabelCount].ForeColor = Color.White;

            label[LabelCount].Text = text;


            if (LabelCount == 14999)
            {
                MessageBox.Show("Contact Admin To increase label count");
                this.Close();
            }
            return LabelCount++;
          
        }
        private int InitLabel(int FontSize, int x, int y, string text)
        {
            label[LabelCount] = new Label();

            switch (FontSize)
            {
                case 8:
                    {
                        label[LabelCount].Font = font8;

                    }
                    break;
                case 10:
                    {
                        label[LabelCount].Font = font10;

                    }
                    break;
                case 12:
                    {
                        label[LabelCount].Font = font12;
                    }
                    break;
                default:
                    {
                        label[LabelCount].Font = new Font("Microsoft Sans Serif", FontSize);
                    }
                    break;

            }



            point.X = x;
            point.Y = y;

            label[LabelCount].Location = point;

            //label[LabelCount].Width = width;

            label[LabelCount].ForeColor = Color.White;

            label[LabelCount].Text = text;
            if (LabelCount == 14999)
            {
                MessageBox.Show("Contact Admin To increase label count");
                this.Close();
            }
            return LabelCount++;
        }
        private int InitPanel(int number, int Width, int Height, int x, int y, int color1, int color2, int color3, bool autoscroll)
        {
            panel[number] = new Panel();

            panel[number].Width = Width;
            panel[number].Height = Height;

            point.X = x;
            point.Y = y;

            panel[number].Location = point;

            panel[number].Visible = true;

            panel[number].BackColor = Color.FromArgb(color1, color2, color3);

            panel[number].AutoScroll = autoscroll;
            

            return number;
        }
        private int InitPanel(int number, int Width, int Height, int x, int y, bool autoscroll)
        {
            panel[number] = new Panel();

            panel[number].Width = Width;
            panel[number].Height = Height;

            point.X = x;
            point.Y = y;

            panel[number].Location = point;

            panel[number].Visible = true;

            //panel[0].BackColor = Color.FromArgb(color1, color2, color3);

            panel[number].AutoScroll = autoscroll;

          
            return number;
        }
        private int InitTextBox(int number, int Width, int FontSize, int x, int y)
        {
            textbox[number] = new System.Windows.Forms.TextBox();

            textbox[number].Width = Width;

            point.X = x;
            point.Y = y;
            textbox[number].Location = point;

            switch (FontSize)
            {
                case 8:
                    {
                        textbox[number].Font = font8;

                    }
                    break;
                case 10:
                    {
                        textbox[number].Font = font10;
                    }
                    break;
                case 12:
                    {
                        textbox[number].Font = font12;
                    }
                    break;
                default:
                    {
                        textbox[number].Font = new Font("Microsoft Sans Serif", FontSize); 
                    }
                    break;
            }

            textbox[number].Text = "";

            return number;
        }
        private int InitButton(int number, int Height, int Width, int x, int y, int FontSize, string Text )
        {


            button[number] = new Button();

            button[number].Height = Height;
            button[number].Width = Width;

            point.X = x;
            point.Y = y;

            button[number].Location = point;

            button[number].Visible = true;

            switch (FontSize)
            {
                case 8:
                    {
                        button[number].Font = font8;
                    }
                    break;
                case 10:
                    {
                        button[number].Font = font10;
                    }
                    break;
                case 12:
                    {
                        button[number].Font = font12;
                    }
                    break;
                default:
                    {
                        button[number].Font = new Font("Microsoft Sans Serif", FontSize);
                    }
                    break;

            }

            //button[number].Font = font8;

            button[number].BackColor = Color.Gainsboro;

            button[number].FlatAppearance.BorderColor = Color.DarkGray;

            button[number].FlatStyle = FlatStyle.Popup;

            button[number].ForeColor = Color.Black;

            button[number].Text = Text;

            

            return number;
        }
        private int InitComboBox(int number, int Width, int x, int y, int FontSize)
        {
            combobox[number] = new ComboBox();

            combobox[number].Width = Width;

            switch (FontSize)
            {
                case 8:
                    {
                        combobox[number].Font = font8;
                    }
                    break;
                case 10:
                    {
                        combobox[number].Font = font10;
                    }
                    break;
                case 12:
                    {
                        combobox[number].Font = font12;
                    }
                    break;
                default:
                    {
                        combobox[number].Font = new Font("Microsoft Sans Serif", FontSize);
                    }
                    break;

            }
            point.X = x;
            point.Y = y;

            combobox[number].Location = point;

            combobox[number].Visible = true;

            return number;
        }
        private int InitDatePicker(int number, int x, int y, int width)
        {
            datepicker[number] = new DateTimePicker();
            point.X = x;
            point.Y = y;

            datepicker[number].Location = point;

            datepicker[number].Width = width;

            datepicker[number].Format = DateTimePickerFormat.Short;

            //datepicker[number].MinDate = DateTime.Today.Date;

            datepicker[number].Visible = true;

            return number;
        }
        private void ClearLabels()
        {
            for (int i = 0; i < LabelCount; i++)
            {
                label[i].Dispose();
            }
        }
        private void ClearTextbox(int number)
        {
            for (int i = 0; i <= number; i++)
            {
                textbox[i].Dispose();
            }
        }
        private void ShowAddTooltip(object sender, EventArgs e)
        {
            Label l = (Label)sender;
           
            try
            {
                tooltip.Dispose();
            }
            catch { }
            tooltip = new ToolTip();
            tooltip.Show(l.Text, panel[ToolTipPanelno], l.Location, 1000);

            //ToolTipPanelno = 0;
            //throw new NotImplementedException();
        }   


        // /////////////////////////////////////////////////////////////////////////////
        private void NewParty1()
        {
            {   // Main Box
                groupbox[0] = new GroupBox();

                groupbox[0].Width = 400;
                groupbox[0].Height = 290;

                point.X = 20;
                point.Y = 10;
                groupbox[0].Location = point;

                groupbox[0].Text = "New Party";

                groupbox[0].ForeColor = Color.White;

                groupbox[0].Font = font12;

                ActivityPanel.Controls.Add(groupbox[0]);


            }

            {   // Name 
                groupbox[0].Controls.Add(label[InitLabel(15, 25, 40, "Name :")]);
                groupbox[0].Controls.Add(textbox[InitTextBox(0, 130, 12, 150, 40)]);
            }

            {   // License
                groupbox[0].Controls.Add(label[InitLabel(15, 25, 90, "Tin no. :")]);
                groupbox[0].Controls.Add(textbox[InitTextBox(1, 130, 12, 150, 90)]);
            }

            {   // Address
                groupbox[0].Controls.Add(label[InitLabel(15, 25, 130, "Address :")]);
                groupbox[0].Controls.Add(textbox[InitTextBox(2, 130, 10, 150, 130)]);
                textbox[2].Multiline = true;
                textbox[2].Height = 60;
            }
            {   // Sale || Purchase Radiobutton
                radiobutton[0] = new RadioButton();

                radiobutton[0].Text = "Sale";

                point.X = 25;
                point.Y = 200;

                radiobutton[0].Location = point;

                radiobutton[0].Width = 70;

                groupbox[0].Controls.Add(radiobutton[0]);

                radiobutton[1] = new RadioButton();

                radiobutton[1].Text = "Purchase";

                point.X = 100;
                point.Y = 200;

                radiobutton[1].Location = point;

                radiobutton[1].Width = 100;

                groupbox[0].Controls.Add(radiobutton[1]);

            }

            {   // Cancel Button

                groupbox[0].Controls.Add(button[InitButton(0, 30, 80, 50, 240, 10, "Cancel")]);
                button[0].Click += NewParty1Cancel;
            }

            {   // Save Button
                groupbox[0].Controls.Add(button[InitButton(1, 30, 80, 150, 240, 10, "Save")]);
                button[1].Click += NewParty1Save;

            }

            textbox[0].Select();


        }                                       // Add New Party
        private void NewParty1Save(object sender, EventArgs e)
        {
            if (textbox[0].Text == "")
            {
                MessageBox.Show("Please Enter Party Name");
                textbox[0].Select();
                // enter name
            }
            else if (textbox[1].Text == "")
            {
                MessageBox.Show("Please Enter Party Tin No.");
                textbox[1].Select();
                // enter license
            }
            else if (textbox[2].Text == "")
            {
                MessageBox.Show("Please Enter Party Address");
                textbox[2].Select();
                // enter address
            }
            else if (radiobutton[0].Checked == false && radiobutton[1].Checked == false)
            {
                MessageBox.Show("Please Select Party Category Sale or Purchase");
                // select sale or purchase
            }
            else
            {
                if (database.AddParty(textbox[0].Text.ToString(), textbox[1].Text.ToString(), textbox[2].Text.ToString(), (radiobutton[0].Checked == true) ? 's' : 'p'))
                {
                    MessageBox.Show("Party Details Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Unable to Save Party Details !!");
                }
                DestroyNewParty1();
                SetHome();
            }
            
            //throw new NotImplementedException();
        }         // Save
        private void NewParty1Cancel(object sender, EventArgs e)
        {

            if (MessageBox.Show("Party Not Saved\n You want to exit", "Cancel Party", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyNewParty1();
                SetHome();
            }
            //throw new NotImplementedException();
        }       // Cancel
        private void DestroyNewParty1()
        {
            ClearLabels();
            ClearTextbox(2);
            radiobutton[0].Dispose();
            radiobutton[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            groupbox[0].Controls.Clear();
            groupbox[0].Dispose();
            ActivityPanel.Controls.Clear();
            
        }                                // Destroy New Party



        // /////////////////////////////////////////////////////////////////////////////
        private void EditParty1()
        {
            {   // Main Panel

                ActivityPanel.Controls.Add(panel[InitPanel(0, 400, 300, 5, 5, false)]);
            }

            {   // Select party To edit label
                panel[0].Controls.Add(label[InitLabel(13, 15, 5, 400, "Select Party To Edit")]);
            }

            {
                panel[0].Controls.Add(label[InitLabel(13, 20, 40, 150, "Party Category :")]);
            }

            {   // Sale Purchase Radiobutton
                radiobutton[0] = new RadioButton();

                radiobutton[0].Text = "Sale";

                point.X = 175;
                point.Y = 40;

                radiobutton[0].Location = point;

                radiobutton[0].Width = 70;

                radiobutton[0].ForeColor = Color.White;

                radiobutton[0].Font = font10;

                radiobutton[0].CheckedChanged += EnableCombo;


                panel[0].Controls.Add(radiobutton[0]);

                radiobutton[1] = new RadioButton();

                radiobutton[1].Text = "Purchase";

                point.X = 250;
                point.Y = 40;

                radiobutton[1].Location = point;

                radiobutton[1].Width = 100;

                radiobutton[1].ForeColor = Color.White;

                radiobutton[1].Font = font10;

                radiobutton[1].CheckedChanged += EnableCombo;

                panel[0].Controls.Add(radiobutton[1]);
            }

            {
                panel[0].Controls.Add(label[InitLabel(13, 20, 90, 100, "Name :")]);
            }

            {
                panel[0].Controls.Add(combobox[InitComboBox(0, 150, 175, 90, 12)]);
                //combobox[0].Items.AddRange(database.GetPartys(()))
                combobox[0].Enabled = false;
            }

            {
                panel[0].Controls.Add(button[InitButton(0, 30, 80, 50, 160, 12, "Edit")]);
                button[0].Click += EditParty1Edit;
            }

            {
                panel[0].Controls.Add(button[InitButton(1, 30, 80, 170, 160, 12, "Cancel")]);
                button[1].Click += EditParty1Cancel;
            }

            {
                panel[0].Controls.Add(button[InitButton(2, 30, 80, 290,160, 12, "Delete")]);
                button[2].ForeColor = Color.Red;
                button[2].Click += EditParty2Delete;
            }
        }
        private void EnableCombo(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            combobox[0].Enabled = true;
            combobox[0].Items.Clear();
            combobox[0].Items.AddRange(database.GetPartys((radiobutton[0].Checked == true) ? 's' : 'p'));

        }
        private void EditParty1Edit(object sender, EventArgs e)
        {
            if (radiobutton[0].Checked == false && radiobutton[1].Checked == false)
            {
                MessageBox.Show("Select Party Category");
            }
            else if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Select Party");
                combobox[0].DroppedDown = true;
            }
            else
            {
                char sp=(radiobutton[0].Checked == true) ? 's' : 'p';
                string[] pinfo = database.PartyInfo(combobox[0].SelectedIndex, sp);
                DestroyEditParty1();
                EditParty2(pinfo,sp);
               
            }


            //throw new NotImplementedException();
        }
        private void EditParty1Cancel(object sender, EventArgs e)
        {
            DestroyEditParty1();
            SetHome();
            //throw new NotImplementedException();
        }
        private void DestroyEditParty1()
        {
            ClearLabels();
            combobox[0].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            button[2].Dispose();
            radiobutton[0].Dispose();
            radiobutton[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
        }
        private void EditParty2Delete(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (radiobutton[0].Checked == false && radiobutton[1].Checked == false)
            {
                MessageBox.Show("Select Party Category");
            }
            else if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Select Party");
                combobox[0].DroppedDown = true;
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete this party", "Delete Party", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (database.DeleteParty(combobox[0].SelectedIndex, (radiobutton[0].Checked == true) ? 's' : 'p'))
                    {
                        MessageBox.Show("Party Deleted Successfully");
                        DestroyEditParty1();
                        SetHome();
                    }
                }
            }
        }



        // /////////////////////////////////////////////////////////////////////////////
        private void EditParty2(string[] info, char sp)
        {
            {
                ActivityPanel.Controls.Add(panel[InitPanel(0, 400, 300, 5, 5, false)]);
            }

            Pid = Convert.ToInt32(info[0]);

            {   // Name 

                panel[0].Controls.Add(label[InitLabel(13, 20, 30, 100, "Name :")]);

                panel[0].Controls.Add(textbox[InitTextBox(0, 150, 12, 200, 30)]);
                textbox[0].Text = info[1];
            }

            {   // License
                panel[0].Controls.Add(label[InitLabel(13, 20, 80, 100, "Tin no. :")]);
                panel[0].Controls.Add(textbox[InitTextBox(1, 150, 12, 200, 80)]);
                textbox[1].Text = info[3];

            }

            {   // Address
                panel[0].Controls.Add(label[InitLabel(13, 20, 130, 100, "Address")]);
                panel[0].Controls.Add(textbox[InitTextBox(2, 150, 12, 200, 130)]);
                textbox[2].Text = info[4];
                textbox[2].Multiline = true;
                textbox[2].Height = 50;
            }

            {
                panel[0].Controls.Add(label[InitLabel(13, 20, 190, "Category")]);
                panel[0].Controls.Add(combobox[InitComboBox(0, 100, 200, 190, 12)]);
                combobox[0].Items.Add("Sale");
                combobox[0].Items.Add("Purchase");
                if (sp == 's')
                {
                    combobox[0].SelectedIndex = 0;
                }
                else
                {
                    combobox[0].SelectedIndex = 1;
                }
            }

            {   // Buttons update & cancel
                panel[0].Controls.Add(button[InitButton(0, 30, 80, 50, 250, 12, "Update")]);
                button[0].Click += EditParty2Update;
                panel[0].Controls.Add(button[InitButton(1, 30, 80, 150, 250, 12, "Cancel")]);
                button[1].Click += EditParty2Cancel;
                
            }
        }
        private void EditParty2Cancel(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("Party Not Updated \nYou want to exit", "Cancel Party Edit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyEditParty2();
                SetHome();
            }
        }
        private void EditParty2Update(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show(combobox[0].SelectedIndex.ToString());
            if (database.UpdateParty(Pid, textbox[0].Text.ToString(), textbox[1].Text.ToString(), textbox[2].Text.ToString(), (combobox[0].SelectedIndex == 0) ? 's' : 'p'))
            {
                MessageBox.Show("Party Details Updated Successfully");
                DestroyEditParty2();
                SetHome();
            }
            else
            {
                MessageBox.Show("Party Details not updated", "Error....");
            }

        }
        private void DestroyEditParty2()
        {
            ClearLabels();
            textbox[0].Dispose();
            textbox[1].Dispose();
            textbox[2].Dispose();
            panel[0].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            Pid = 0;
        }



        // /////////////////////////////////////////////////////////////////////////////
        private void ViewPartys1()
        {
            string[] info = new string[5];
            int y = 10;
            ToolTipPanelno = 0;
            {   // Main Panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 250, 5, 5, 45, 45, 45, true)]);
            }

            {
                ActivityPanel.Controls.Add(button[InitButton(0, 30, 80, 400, 280, 10, "Exit")]);
                button[0].Click += ViewParty1Exit;
            }

            {   // Sr. 
                panel[0].Controls.Add(label[InitLabel(10, 5, y, 50, "Sr.")]);
            }

            {   // Name
                panel[0].Controls.Add(label[InitLabel(10, 55, y, 90, "Name")]);
            }

            {   // License 
                panel[0].Controls.Add(label[InitLabel(10, 150, y, 90, "Tin no.")]);
            }

            {   // Address
                panel[0].Controls.Add(label[InitLabel(10, 245, y, 150, "Address")]);
            }

            {   // Category
                panel[0].Controls.Add(label[InitLabel(10, 400, y, 70, "Category")]);
            }

            //{   // Edit
            //    panel[0].Controls.Add(label[InitLabel(10, 415, y, 60, "Edit")]);
            //}

            y += 30;
            for (int i = 0; i < database.GetPartyNo('s'); i++)
            {
                info = database.PartyInfo(i, 's');

                {   // Sr. 
                    panel[0].Controls.Add(label[InitLabel(10, 5, y, 50, (i + 1).ToString())]);
                }

                {   // Name
                    panel[0].Controls.Add(label[InitLabel(10, 55, y, 90, info[1])]);
                }

                {   // License 
                    panel[0].Controls.Add(label[InitLabel(10, 150, y, 90, info[3])]);
                }

                {   // Address
                    panel[0].Controls.Add(label[InitLabel(10, 245, y, 150, info[4])]);
                    label[LabelCount - 1].MouseEnter += ShowAddTooltip;
                    //label[LabelCount - 1].mo += HideAddTooltip;
                }

                {   // Category
                    panel[0].Controls.Add(label[InitLabel(10, 400, y, 70, "Sale")]);
                }

                //{   // Edit
                //    panel[0].Controls.Add(label[InitLabel(10, 415, y, 60, "Edit "+(y/30).ToString())]);
                //    label[LabelCount - 1].BorderStyle = BorderStyle.Fixed3D;
                //    label[LabelCount-1].Click+=EditParty;


                //}

                y += 30;
            }



            for (int i = 0; i < database.GetPartyNo('p'); i++)
            {
                info = database.PartyInfo(i, 'p');

                {   // Sr. 
                    panel[0].Controls.Add(label[InitLabel(10, 5, y, 50, (y / 30).ToString())]);
                }

                {   // Name
                    panel[0].Controls.Add(label[InitLabel(10, 55, y, 90, info[1])]);
                }

                {   // License 
                    panel[0].Controls.Add(label[InitLabel(10, 150, y, 90, info[3])]);
                }

                {   // Address
                    panel[0].Controls.Add(label[InitLabel(10, 245, y, 150, info[4])]);
                    label[LabelCount - 1].MouseEnter += ShowAddTooltip;

                }

                {   // Category
                    panel[0].Controls.Add(label[InitLabel(10, 400, y, 70, "Purchase")]);
                }


                //{   // Edit
                //    panel[0].Controls.Add(label[InitLabel(10, 415, y, 60, "Edit " + (y / 30).ToString())]);
                //    label[LabelCount - 1].BorderStyle = BorderStyle.Fixed3D;
                //    label[LabelCount - 1].Click += EditParty;
                //    //panel[0].Controls.Add(label[InitLabel(10,)])
                //}

                y += 30;
            }
        }
        private void DestroyViewParty1()
        {
            ClearLabels();
            button[0].Dispose();
            tooltip.Dispose();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
            ToolTipPanelno = 0;
        }
        private void ViewParty1Exit(object sender, EventArgs e)
        {
            DestroyViewParty1();
            SetHome();
            //throw new NotImplementedException();
        }
        




        // /////////////////////////////////////////////////////////////////////////////
        private void NewItem1()
        {

            {   // Main Box
                groupbox[0] = new GroupBox();

                groupbox[0].Width = 400;
                groupbox[0].Height = 290;

                point.X = 20;
                point.Y = 10;
                groupbox[0].Location = point;

                groupbox[0].Text = "New Item";

                groupbox[0].ForeColor = Color.White;

                groupbox[0].Font = font12;

                ActivityPanel.Controls.Add(groupbox[0]);


            }

            {   // Name 
                groupbox[0].Controls.Add(label[InitLabel(15, 25, 40, "Name :")]);
                groupbox[0].Controls.Add(textbox[InitTextBox(0, 130, 12, 250, 40)]);
            }


            {   // Measurement Scale
                groupbox[0].Controls.Add(label[InitLabel(15, 25, 90, 200, "Measurement Scale :")]);
                groupbox[0].Controls.Add(combobox[InitComboBox(0, 120, 250, 90, 12)]);

                combobox[0].Items.AddRange(database.GetMeasurementScales());

            }


            {   // Sale || Purchase Radiobutton
                radiobutton[0] = new RadioButton();

                radiobutton[0].Text = "Sale";

                point.X = 25;
                point.Y = 140;

                radiobutton[0].Location = point;

                radiobutton[0].Width = 70;

                groupbox[0].Controls.Add(radiobutton[0]);

                radiobutton[1] = new RadioButton();

                radiobutton[1].Text = "Purchase";

                point.X = 100;
                point.Y = 140;

                radiobutton[1].Location = point;

                radiobutton[1].Width = 100;

                groupbox[0].Controls.Add(radiobutton[1]);

            }


            {   // Cancel Button

                groupbox[0].Controls.Add(button[InitButton(0, 30, 80, 50, 200, 10, "Cancel")]);
                button[0].Click += NewItem1Cancel;
            }

            {   // Save Button
                groupbox[0].Controls.Add(button[InitButton(1, 30, 80, 150, 200, 10, "Save")]);
                button[1].Click += NewItem1Save;

            }

            textbox[0].Select();


        }
        private void NewItem1Save(object sender, EventArgs e)
        {
            if (textbox[0].Text == "")
            {
                MessageBox.Show("Please Enter Item Name");
                textbox[0].Select();
            }
            else if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Measurement Scale ");
                combobox[0].DroppedDown = true;
            }
            else if (radiobutton[0].Checked == false && radiobutton[1].Checked == false)
            {
                MessageBox.Show("Please Select Item Category Sale or Purchase");

            }
            else
            {
                if (database.AddItem(textbox[0].Text.ToString(), database.GetMeasurementScaleid(combobox[0].Text.ToString()), (radiobutton[0].Checked == true) ? 's' : 'p'))
                {
                    MessageBox.Show("Item Details Saved Successfully ");
                }
                else
                {
                    MessageBox.Show("Unable to save Item Details");
                }
                DestroyNewItem1();
                SetHome();
            }
            //throw new NotImplementedException();
        }
        private void NewItem1Cancel(object sender, EventArgs e)
        {
            if (MessageBox.Show("Item Not Saved \n You want to exit", "Cancel Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyNewItem1();
                SetHome();
            }
            //throw new NotImplementedException();
        }
        private void DestroyNewItem1()
        {
            ClearLabels();
            ClearTextbox(0);
            combobox[0].Dispose();
            radiobutton[0].Dispose();
            radiobutton[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            groupbox[0].Controls.Clear();
            groupbox[0].Dispose();
            ActivityPanel.Controls.Clear();
            
        }


        // /////////////////////////////////////////////////////////////////////////////
        private void ViewItem1()
        {
            int y = 5;
            string[] info = new string[4];
            {   // Main panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 250, 5, 5, true)]);
            }

            {   // Sr.
                panel[0].Controls.Add(label[InitLabel(12, 5, y, 80, "Sr.")]);
            }

            {   // Name 
                panel[0].Controls.Add(label[InitLabel(12, 90, y, 150, "Name")]);
            }

            {   // Measurement Scale
                panel[0].Controls.Add(label[InitLabel(12, 245, y, 150, "Scale")]);
            }

            {   // Category
                panel[0].Controls.Add(label[InitLabel(12, 400, y, 80, "Category")]);
            }

            for (int i = 0; i < database.GetItemNo('s'); i++)
            {
                y += 30;
                info = database.ItemInfo(i, 's');


                {   // Sr.
                    panel[0].Controls.Add(label[InitLabel(12, 5, y, 80, (i+1).ToString())]);
                }

                {   // Name 
                    panel[0].Controls.Add(label[InitLabel(12, 90, y, 150, info[1])]);
                }

                {   // Measurement Scale
                    panel[0].Controls.Add(label[InitLabel(12, 245, y, 150,database.GetMeasurementScaleName(Convert.ToInt32(info[2])))]);
                }

                {   // Category
                    panel[0].Controls.Add(label[InitLabel(12, 400, y, 80, "Sale")]);
                }


            }

            for (int i = 0; i < database.GetItemNo('p'); i++)
            {
                y += 30;
                info = database.ItemInfo(i, 'p');


                {   // Sr.
                    panel[0].Controls.Add(label[InitLabel(12, 5, y, 80, (y/30).ToString())]);
                }

                {   // Name 
                    panel[0].Controls.Add(label[InitLabel(12, 90, y, 150, info[1])]);
                }

                {   // Measurement Scale
                    panel[0].Controls.Add(label[InitLabel(12, 245, y, 150, database.GetMeasurementScaleName(Convert.ToInt32(info[2])))]);
                }

                {   // Category
                    panel[0].Controls.Add(label[InitLabel(12, 400, y, 80, "Purchase")]);
                }


            }




            {
                ActivityPanel.Controls.Add(button[InitButton(0, 30, 80, 400, 280, 12, "Exit")]);
                button[0].Click += ViewItem1Exit;
            }
        }   
        private void ViewItem1Exit(object sender, EventArgs e)
        {
            DestroyViewItem1();
            SetHome();

            //throw new NotImplementedException();
        }
        private void DestroyViewItem1()
        {
            ClearLabels();
            button[0].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();

        }



        // /////////////////////////////////////////////////////////////////////////////
        private void EditItem1()
        {
            {   // Main Panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 300, 5, 5, 45, 45, 45, false)]);
            }

            {   // Select Item Label
                panel[0].Controls.Add(label[InitLabel(12, 5, 5, 200, "Select Item")]);
            }

            {   // Item Category
                panel[0].Controls.Add(label[InitLabel(12, 20, 40, 150, "Category :")]);
                panel[0].Controls.Add(combobox[InitComboBox(0, 100, 200, 40, 10)]);
                combobox[0].Items.Add("Sale");
                combobox[0].Items.Add("Purchase");
                combobox[0].SelectedIndexChanged += EditItem1CatSelected;
            }

            {   // Item Name
                panel[0].Controls.Add(label[InitLabel(12, 20, 90, 150, "Name :")]);
                panel[0].Controls.Add(combobox[InitComboBox(1, 100, 200, 90, 10)]);
                combobox[1].Enabled = false;
            }

            {   // buttons Edit & Cancel  & Delete
                panel[0].Controls.Add(button[InitButton(0, 30, 80, 30, 140, 12, "Edit")]);
                button[0].Click += EditItem1Edit;
                panel[0].Controls.Add(button[InitButton(1, 30, 80, 130, 140, 12, "Cancel")]);
                button[1].Click += EditItem1Cancel;
                panel[0].Controls.Add(button[InitButton(2, 30, 80, 230, 140, 12, "Delete")]);
                button[2].ForeColor = Color.Red;
                button[2].Click += EditItem1Delete;

            }
        }
        private void EditItem1Delete(object sender, EventArgs e)
        {
            if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Select Item Category");
                combobox[0].DroppedDown = true;
            }
            else if (combobox[1].SelectedIndex < 0)
            {
                MessageBox.Show("Select Item");
                combobox[1].DroppedDown = true;
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete this Item", "Delete Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (database.DeleteItem(combobox[1].SelectedIndex, (combobox[0].SelectedIndex == 0) ? 's' : 'p'))
                    {
                        MessageBox.Show("Item Deleted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Unable to Delete Item");//+database.LoginError.ToString());
                    }
                    DestroyEditItem1();
                    SetHome();
                }
            }
            //throw new NotImplementedException();
        }
        private void EditItem1Edit(object sender, EventArgs e)
        {
            //int id = 0;

            if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Select Item Category");
                combobox[0].DroppedDown = true;
            }
            else if (combobox[1].SelectedIndex < 0)
            {
                MessageBox.Show("Select Item");
                combobox[1].DroppedDown = true;
            }
            else
            {
                int id=combobox[1].SelectedIndex;
                char sp = (combobox[0].SelectedIndex == 0) ? 's' : 'p';
                DestroyEditItem1();
                EditItem2(database.ItemInfo(id,sp),sp);
            }
            //throw new NotImplementedException();
        }
        private void EditItem1Cancel(object sender, EventArgs e)
        {
            DestroyEditItem1();
            SetHome();
            //throw new NotImplementedException();
        }
        private void EditItem1CatSelected(object sender, EventArgs e)
        {
            combobox[1].Enabled = true;
            combobox[1].Items.Clear();
            switch (combobox[0].SelectedIndex)
            {
                case 0:
                    {
                        combobox[1].Items.AddRange(database.GetItems('s'));
                    }
                    break;
                case 1:
                    {
                        combobox[1].Items.AddRange(database.GetItems('p'));
                    }
                    break;
            }
            //throw new NotImplementedException();
        }
        private void DestroyEditItem1()
        {
            ClearLabels();
            combobox[0].Dispose();
            combobox[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
        }



        // /////////////////////////////////////////////////////////////////////////////
        private void EditItem2(string[] info,char sp)
        {
            {   // Main Panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 300, 5, 5, 45, 45, 45, false)]);
            }

            {   // Edit Item Label
                panel[0].Controls.Add(label[InitLabel(13, 5, 5, 200, "Edit Item")]);
            }

            {   // Item Category
                panel[0].Controls.Add(label[InitLabel(12, 20, 40, 150, "Category")]);
                panel[0].Controls.Add(combobox[InitComboBox(0, 100, 200, 40, 10)]);
                combobox[0].Items.Add("Sale");
                combobox[0].Items.Add("Purchase");
                combobox[0].SelectedIndex = (sp == 's') ? 0 : 1;
            }

            {   // Item Name
                panel[0].Controls.Add(label[InitLabel(12, 20, 90, 150, "Name")]);
                panel[0].Controls.Add(textbox[InitTextBox(0, 150, 10, 200, 90)]);
                textbox[0].Text = info[1];
            }

            {   // Measurement Scale
                panel[0].Controls.Add(label[InitLabel(12, 20, 140, 150, "Measurement Scale")]);
                panel[0].Controls.Add(combobox[InitComboBox(1, 100, 200, 140, 10)]);
                combobox[1].Items.AddRange(database.GetMeasurementScales());
                combobox[1].SelectedItem = database.GetMeasurementScaleName(Convert.ToInt32(info[2]));
            }

            {   // Buttons Update & Cancel

                panel[0].Controls.Add(button[InitButton(0, 30, 80, 30, 190, 12, "Update")]);
                button[0].Click += EditItem2Update;
                panel[0].Controls.Add(button[InitButton(1, 30, 80, 130, 190, 12, "Cancel")]);
                button[1].Click += EditItem2Cancel;
            }
            Pid = Convert.ToInt32(info[0]);
        }
        private void EditItem2Cancel(object sender, EventArgs e)
        {
            if (MessageBox.Show("Item Not Update \n You want to cancel and Exit", "Edit Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyEditItem2();
                SetHome();
            }
            //throw new NotImplementedException();
        }
        private void EditItem2Update(object sender, EventArgs e)
        {
            if (database.UpdateItem(Pid, textbox[0].Text.ToString(), database.GetMeasurementScaleid(combobox[1].Text), (combobox[0].SelectedIndex == 0) ? 's' : 'p'))
            {
                MessageBox.Show("Item Details Updated Successfully");
                DestroyEditItem2();
                SetHome();
            }
            else
            {
                MessageBox.Show("Unable to Update Item Details");
            }

            
            //throw new NotImplementedException();
        }
        private void DestroyEditItem2()
        {
            ClearLabels();
            combobox[0].Dispose();
            combobox[1].Dispose();
            textbox[0].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
            Pid = 0;
                
        }

        // /////////////////////////////////////////////////////////////////////////////

        private void NewSale1()
        {
            SelectPartyPanel('s');
            combobox[0].SelectedIndexChanged += NewSale1PartySelected;
            button[1].Click += NewSale1Next; 
            button[0].Click += NewSale1Cancel;
            combobox[0].Select();

        }   
        private void NewSale1Cancel(object sender, EventArgs e)
        {
            DestroyNewSale1();
            SetHome();
            
            //throw new NotImplementedException();
        }   
        private void NewSale1Next(object sender, EventArgs e)
        {
            try
            {
                tooltip.Dispose();
            }
            catch { }
            if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Party");
                combobox[0].DroppedDown = true;
            }
            else
            {
                DestroyNewSale1();
                NewSale2();
                //newsale = new NewSale();
                //newsale.PartyId=
            }
            //throw new NotImplementedException();
        }   
        private void NewSale1PartySelected(object sender, EventArgs e)
        {
            ToolTipPanelno = 1;
            string[] info = database.PartyInfo(combobox[0].SelectedIndex, 's');
            Pid = Convert.ToInt32(info[0]);
            panel[1].Controls.Clear();
            panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, info[1])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, info[3])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 90, 300, info[4])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            


            //newsale.Reset();
            //newsale.PInfo=database.PartyInfo(combobox[0].SelectedIndex, 's');
            //newsale.PartyId = Convert.ToInt32(newsale.PInfo[0]);
            //panel[1].Controls.Clear();
            //panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, newsale.PInfo[1])]);
            //panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, newsale.PInfo[3])]);
            //panel[1].Controls.Add(label[InitLabel(12,5,90,newsale.PInfo[4])]);
            ////throw new NotImplementedException();
        }
        private void DestroyNewSale1()
        {
            ClearLabels();
            combobox[0].Dispose();
            try
            {
                tooltip.Dispose();
            }
            catch { }
            panel[1].Controls.Clear();
            panel[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
            ToolTipPanelno = 0;

        }

                
        // /////////////////////////////////////////////////////////////////////////////
        private void NewSale2()
        {

            // Create Entry in database for new bill n get billno(_id)
            database.CurrentBillno = database.NewBill(Pid, 's');
            if (database.CurrentBillno <= 0) ;
            database.ItemNo = 0;
            database.BillTotal = 0;
            string[] info = database.PartyInfo(Pid);

            {   // Bill no  r no and date
                ActivityPanel.Controls.Add(label[InitLabel(12, 5, 5, 90, "Bill no." + database.CurrentBillno.ToString())]);
                ActivityPanel.Controls.Add(label[InitLabel(12, 100, 5, 50, "R no.")]);
                numericupdown[2] = new NumericUpDown();
                numericupdown[2].Font = font10;
                point.X = 160;
                point.Y = 5;
                numericupdown[2].Location = point;
                numericupdown[2].Width = 50;
                numericupdown[2].Maximum = 9999;
                numericupdown[2].Enter += SelectNUD0Text;

                ActivityPanel.Controls.Add(numericupdown[2]);

                ActivityPanel.Controls.Add(label[InitLabel(12, 160, 5, 50, "Date :")]);
                ActivityPanel.Controls.Add(datepicker[InitDatePicker(0, 220, 5, 50)]);
            }

            {   // Party Name Label
                ActivityPanel.Controls.Add(label[InitLabel(12, 350, 5, 200, info[1])]);
            }

            {   // New Item Panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 50, 10, 35, 45, 45, 45, false)]);
                SetS2Panel0();
            }

            {   // Items Panel
                ActivityPanel.Controls.Add(panel[InitPanel(1, 300, 170, 10, 100, 45, 45, 45, true)]);
                SetS2Panel1();
            }

            {   // Summary Panel
                ActivityPanel.Controls.Add(panel[InitPanel(2, 190, 180, 320, 100, 45, 45, 45, false)]);
                SetS2Panel2();
            }

            {   // Summary pannel2
                ActivityPanel.Controls.Add(panel[InitPanel(3, 300, 40, 10, 280, 45, 45, 45, false)]);
                SetS2Panel3();
            }

            {   // Cancel Button
                ActivityPanel.Controls.Add(button[InitButton(2, 30, 80, 320, 290, 12, "Cancel")]);
                button[2].Click += NewSale2Cancel;
            }

            {   // Next Button
                ActivityPanel.Controls.Add(button[InitButton(3, 30, 80, 420, 290, 12, "Next")]);
                button[3].Click += NewSale2Next;
            }
            combobox[0].Select();

        }                                  // New Sale Step 2
        private void NewSale2Next(object sender, EventArgs e)
        {
            int billno = 0;
            if (database.ItemNo == 0)
            {
                MessageBox.Show("Cant make bill with 0 items");
            }
            else
            {
                billno = database.CurrentBillno;
                //database.MakeBill(database.CurrentBillno, 's', database.ItemNo, database.BillTotal);
                if (database.MakeBill('s', datepicker[0].Value.Date.ToShortDateString(), Convert.ToInt32(textbox[2].Text), Convert.ToInt32(textbox[3].Text), Convert.ToInt32(textbox[4].Text), Convert.ToInt32(textbox[5].Text), textbox[6].Text.ToString(), textbox[7].Text.ToString(), textbox[8].Text.ToString(), Convert.ToInt32(numericupdown[2].Value)))
                {
                    DestroySale2();
                    NewSale3(billno);
                }
                else
                {
                    MessageBox.Show("Unable to save bill due to connection problem with database", "Error");
                    DestroySale2();
                    SetSale();
                }
                                
            }
            
            //throw new NotImplementedException();
        }    // Next
        private void NewSale2Cancel(object sender, EventArgs e)
        {
            database.CancelBill(database.CurrentBillno, 's');
            database.CurrentBillno = 0;
            DestroySale2();
            SetHome();

            //throw new NotImplementedException();
        }  // Cancel
        private void SetS2Panel0()
        {
            {   // Item Name
                panel[0].Controls.Add(combobox[InitComboBox(0, 100, 5, 10, 10)]);
                combobox[0].Items.AddRange(database.GetItems('s'));
            }

            {   //  Qty Label
                panel[0].Controls.Add(label[InitLabel(10,120,10,40,"Qty :")]);
            }

            {   // Item Qty
                numericupdown[0] = new NumericUpDown();
                numericupdown[0].Font = font10;
                point.X = 170;
                point.Y = 10;
                numericupdown[0].Location = point;
                numericupdown[0].Width = 50;
                numericupdown[0].Maximum = 9999;
                numericupdown[0].Enter += SelectNUD0Text;
                panel[0].Controls.Add(numericupdown[0]);
            }

            {   //  Price Label
                panel[0].Controls.Add(label[InitLabel(10,230,10,50,"Price")]);
            }

            {   //  Item Price
                numericupdown[1] = new NumericUpDown();
                numericupdown[1].Font = font10;
                point.X = 290;
                point.Y = 10;
                numericupdown[1].Location = point;
                numericupdown[1].Width = 50;
                numericupdown[1].Maximum = 10000;
                numericupdown[1].Increment = 10;
                numericupdown[0].Enter += SelectNUD1Text;
                panel[0].Controls.Add(numericupdown[1]);
            }
            
            {   // Add Button
                panel[0].Controls.Add(button[InitButton(0, 30, 35, 400, 10, 12, "+")]);
                button[0].BackColor = Color.Silver;
                button[0].FlatAppearance.BorderColor = Color.DarkGray;
                button[0].FlatStyle = FlatStyle.Popup;
                button[0].ForeColor = Color.Green;
                button[0].Click += S2AddItem;
            }

            {   // Cancel Button
                panel[0].Controls.Add(button[InitButton(1, 30, 35, 450, 10, 12, "x")]);
                button[1].BackColor = Color.Silver;
                button[1].FlatAppearance.BorderColor = Color.DarkGray;
                button[1].FlatStyle = FlatStyle.Popup;
                button[1].ForeColor = Color.Red;
                button[1].Click += S2NICancel;

            }


        }                               // New Item Panel
        private void S2AddItem(object sender, EventArgs e)
        {

            if (database.ItemNo >= 20)
            {
                MessageBox.Show("Cant have more than 20 items in single bill");
                ClearS2NewItemControls();
            }

            else if (combobox[0].SelectedIndex <0)
            {
                MessageBox.Show("Select item from list ");
                combobox[0].DroppedDown = true;
            }
            else if (Convert.ToInt32( numericupdown[0].Value) == 0)
            {
                MessageBox.Show("Qty cant be 0");
                numericupdown[0].Select();
            }
            else if (Convert.ToInt32(numericupdown[1].Value) == 0)
            {
                MessageBox.Show("Price cant be 0");
                numericupdown[1].Select();
            }
            else if (database.AddItemToBill(database.CurrentBillno, database.GetItemId(combobox[0].Text, 's'), Convert.ToInt32(numericupdown[0].Value), Convert.ToInt32(numericupdown[1].Value), 's'))
            {
                panel[1].VerticalScroll.Value = 0;
                panel[1].Controls.Add(label[InitLabel(10, 3, 25 + (database.ItemNo * 25), 27, (database.ItemNo + 1).ToString())]);
                panel[1].Controls.Add(label[InitLabel(10, 35, 25 + (database.ItemNo * 25), 75, combobox[0].Text.ToString())]);
                label[LabelCount - 1].MouseEnter += ShowAddTooltip;
                panel[1].Controls.Add(label[InitLabel(10, 115, 25 + (database.ItemNo * 25), 55, Convert.ToInt32(numericupdown[0].Value).ToString())]);
                panel[1].Controls.Add(label[InitLabel(10, 175, 25 + (database.ItemNo * 25), 55, Convert.ToInt32(numericupdown[1].Value).ToString())]);
                panel[1].Controls.Add(label[InitLabel(10, 235, 25 + (database.ItemNo * 25), 55, (Convert.ToInt32(numericupdown[0].Value)*Convert.ToInt32(numericupdown[1].Value)).ToString())]);


                database.ItemNo++;
                database.BillTotal += Convert.ToInt32(numericupdown[0].Value) * Convert.ToInt32(numericupdown[1].Value);
                ClearS2NewItemControls();
                UpdateS2Panel2();
            }
            else
            {
                MessageBox.Show(database.LoginError.ToString());
            }


            //throw new NotImplementedException();
        }       // Add New Item (+ click)
        private void SetS2Panel1()
        {
            {       // Sr.                
                panel[1].Controls.Add(label[InitLabel(10, 3, 2, 27, "Sr.")]);
            }   // 3-30

            {       // Name
                panel[1].Controls.Add(label[InitLabel(10, 35, 2, 75, "Name")]);
            }   // 35-110

            {       // Qty
                panel[1].Controls.Add(label[InitLabel(10, 115, 2, 55, "Qty.")]);
            }   // 115-170

            {       // Price
                panel[1].Controls.Add(label[InitLabel(10, 175, 2, 55, "Price")]);
            }   // 175-210

            {       // Total
                panel[1].Controls.Add(label[InitLabel(10, 235, 2, 55, "Total")]);
            }   // 235-270



        }                               // All Items Panel
        private void SetS2Panel2()
        {
            panel[2].Controls.Add(label[InitLabel(10, 5, 5, 80, "Items")]);
            panel[2].Controls.Add(textbox[InitTextBox(0, 50, 10, 100, 5)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 35, 80, "iTotal")]);
            panel[2].Controls.Add(textbox[InitTextBox(1, 50, 10, 100, 35)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 65, 80, "Vat")]);
            panel[2].Controls.Add(textbox[InitTextBox(2, 50, 10, 100, 65)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 95, 80, "Postage")]);
            panel[2].Controls.Add(textbox[InitTextBox(3, 50, 10, 100, 95)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 125, 80, "Himali")]);
            panel[2].Controls.Add(textbox[InitTextBox(4, 50, 10, 100, 125)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 155, 80, "Total")]);
            panel[2].Controls.Add(textbox[InitTextBox(5, 50, 10, 100, 155)]);
            

            for (int i = 0; i < 6; i++)
            {
                if (i > 1 && i < 5)
                {
                    textbox[i].KeyUp += UpdateS2Panel2;
                    continue;
                }
                textbox[i].ForeColor = Color.White;
                textbox[i].Enabled = false;
                textbox[i].Text = "0";
            }
        }
        private void UpdateS2Panel2(object sender, KeyEventArgs e)
        {
            UpdateS2Panel2();
        }
        private void SetS2Panel3()
        {
            panel[3].Controls.Add(label[InitLabel(10, 0, 5, 40, "PM")]);
            panel[3].Controls.Add(textbox[InitTextBox(6, 45, 10, 40, 5)]);
            panel[3].Controls.Add(label[InitLabel(10, 90, 5, 40, "GR")]);
            panel[3].Controls.Add(textbox[InitTextBox(7, 45, 10, 135, 5)]);
            panel[3].Controls.Add(label[InitLabel(10, 185, 5, 65, "Through")]);
            panel[3].Controls.Add(textbox[InitTextBox(8, 50, 10, 250, 5)]);
            
        }                               // Summary Panel2
        private void UpdateS2Panel2()
        {
            textbox[0].Text = database.ItemNo.ToString();
            textbox[1].Text = database.BillTotal.ToString();
            if (textbox[2].Text == "")
                textbox[2].Text = "0";
            if (textbox[3].Text == "")
                textbox[3].Text = "0";
            if (textbox[4].Text == "")
                textbox[4].Text = "0";

            textbox[5].Text = (database.BillTotal + Convert.ToInt32(textbox[2].Text) + Convert.ToInt32(textbox[3].Text) + Convert.ToInt32(textbox[4].Text)).ToString();
        }                            // Summary Panel
        private void S2NICancel(object sender, EventArgs e)
        {
            ClearS2NewItemControls();
            //throw new NotImplementedException();
        }      // New Item Cancel
        private void ClearS2NewItemControls()
        {
            combobox[0].Text = "";
            numericupdown[0].Value = 0;
            numericupdown[1].Value = 0;

            combobox[0].Select();
        }                    // Clear New Item Controls
        private void SelectNUD0Text(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            numericupdown[0].Select(0, 4);           // Select its value
        }  // When NewSale2 NumerciUpdown[0] is selected 
        private void SelectNUD1Text(object sender, EventArgs e)
        {
            numericupdown[1].Select(0, 5);
        }  // When NewSale2 NumerciUpdown[1] is selected 
        private void DestroySale2()
        {
            ClearLabels();
            combobox[0].Dispose();
            numericupdown[0].Dispose();
            numericupdown[1].Dispose();
            numericupdown[2].Dispose();
            datepicker[0].Dispose();
            for (int i = 0; i <= 8; i++)
            {
                textbox[i].Dispose();
                if (i > 3)
                    continue;
                button[i].Dispose();
            }
            panel[0].Dispose();
            panel[1].Dispose();
            panel[2].Dispose();
            panel[3].Dispose();
            ActivityPanel.Controls.Clear();
            database.CurrentBillno = 0;
            database.ItemNo = 0;
            database.BillTotal = 0;
        }                              // Clear Unwanted Controls

        //  //////////////////////////////////////////////////////////////////////////////////////

        private void NewSale3(int billno)
        {
            int e=database.MakePdfBill(billno, 's');

            if (e == 1)
            {
                MessageBox.Show("Bill pdf created successfully");
                System.Diagnostics.Process.Start(Application.StartupPath+"/Bills/Sales/Billno"+billno+".pdf");   
                
            }
            else if (e == -1)
            {
                MessageBox.Show("Bills/Sales/Billno" + billno + ".pdf\nFile Already Open in other program");
            }
            else if (e == 0)
            {
                MessageBox.Show("Bill no."+billno+" Does not Exit in Database", "Database Error");
            }
            SetHome();

        }                        // Makes pdf of bill and opens in default program

            
      
        //  //////////////////////////////////////////////////////////////////////////////////////
        private void SelectPartyPanel(char BillType)
        {
            string Bill = "";
            switch (BillType)
            {
                case 's':
                    {
                        Bill = "New Sale";
                    }
                    break;
                case 'p':
                    {
                        Bill = "New Purchase";
                    }
                    break;
                case 'r':
                    {// Payment Sale
                        BillType = 's';
                        Bill = "Payment Sale";
                    }
                    break;
                case 'o':
                    {   // Payment Purchase
                        BillType = 'p';
                        Bill = "Payment Purchase";
                    }
                    break;

            }

            {   // Main Panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 300, 5, 5, 45, 45, 45, false)]);
            }

            {   // New Bill & Select Party label
                panel[0].Controls.Add(label[InitLabel(12, 5, 5, 200, Bill)]);
                panel[0].Controls.Add(label[InitLabel(12, 15, 40, 200, "Select Party")]);
            }

            {   // Party Name
                panel[0].Controls.Add(label[InitLabel(12, 20, 90, 150, "Name :")]);
                panel[0].Controls.Add(combobox[InitComboBox(0, 150, 200, 90, 10)]);
                combobox[0].Items.AddRange(database.GetPartys(BillType));
                
            }

            {   // Info Panel
                panel[0].Controls.Add(panel[InitPanel(1, 450, 130, 20, 130, 50, 50, 50, true)]);
            }

            {   // Buttons Next & Cancel
                panel[0].Controls.Add(button[InitButton(1, 30, 80, 350, 265, 12, "Next")]);
                
                panel[0].Controls.Add(button[InitButton(0, 30, 80, 250, 265, 12, "Cancel")]);
            }
        }
        private void DestroyPartyPanel()
        {
            ClearLabels();

            combobox[0].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[1].Dispose();
            panel[0].Dispose();
        }
        private void SelectItemsPanel(char BillType)
        {
        }



        // /////////////////////////////////////////////////////////////////////////////////////////

        
        private void NewPurchase1()
        {
            SelectPartyPanel('p');
            combobox[0].SelectedIndexChanged += NewPurchase1PartySelected;
            button[1].Click += NewPurchase1Next;
            button[0].Click += NewPurchase1Cancel;
            combobox[0].Select();

        }
        private void NewPurchase1Cancel(object sender, EventArgs e)
        {
            DestroyNewPurchase1();
            SetHome();

            //throw new NotImplementedException();
        }
        private void NewPurchase1Next(object sender, EventArgs e)
        {
            try
            {
                tooltip.Dispose();
            }
            catch { }
            if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Party");
                combobox[0].DroppedDown = true;
            }
            else
            {
                DestroyNewPurchase1();
                NewPurchase2();
                //newsale = new NewSale();
                //newsale.PartyId=
            }
            //throw new NotImplementedException();
        }
        private void NewPurchase1PartySelected(object sender, EventArgs e)
        {
            ToolTipPanelno = 1;
            string[] info = database.PartyInfo(combobox[0].SelectedIndex, 'p');
            Pid = Convert.ToInt32(info[0]);
            panel[1].Controls.Clear();
            panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, info[1])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, info[3])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 90, 300, info[4])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;



            //newsale.Reset();
            //newsale.PInfo=database.PartyInfo(combobox[0].SelectedIndex, 's');
            //newsale.PartyId = Convert.ToInt32(newsale.PInfo[0]);
            //panel[1].Controls.Clear();
            //panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, newsale.PInfo[1])]);
            //panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, newsale.PInfo[3])]);
            //panel[1].Controls.Add(label[InitLabel(12,5,90,newsale.PInfo[4])]);
            ////throw new NotImplementedException();
        }
        private void DestroyNewPurchase1()
        {
            ClearLabels();
            combobox[0].Dispose();
            try
            {
                tooltip.Dispose();
            }
            catch { }
            panel[1].Controls.Clear();
            panel[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
            ToolTipPanelno = 0;

        }


        // /////////////////////////////////////////////////////////////////////////////
        private void NewPurchase2()
        {

            // Create Entry in database for new bill n get billno(_id)
            database.CurrentBillno = database.NewBill(Pid, 'p');
            if (database.CurrentBillno <= 0) ;
            database.ItemNo = 0;
            database.BillTotal = 0;
            string[] info = database.PartyInfo(Pid);

            {   // Bill no  date
                ActivityPanel.Controls.Add(label[InitLabel(12, 5, 5, 90, "Bill no." + database.CurrentBillno.ToString())]);

                ActivityPanel.Controls.Add(label[InitLabel(12, 100, 5, 50, "R no.")]);
                numericupdown[2] = new NumericUpDown();
                numericupdown[2].Font = font10;
                point.X = 160;
                point.Y = 5;
                numericupdown[2].Location = point;
                numericupdown[2].Width = 50;
                numericupdown[2].Maximum = 9999;
                numericupdown[2].Enter += SelectNUD0Text;

                ActivityPanel.Controls.Add(numericupdown[2]);


                ActivityPanel.Controls.Add(label[InitLabel(12, 160, 5, 50, "Date :")]);
                ActivityPanel.Controls.Add(datepicker[InitDatePicker(0, 220, 5, 50)]);
            }

            {   // Party Name Label
                ActivityPanel.Controls.Add(label[InitLabel(12, 350, 5, 200, info[1])]);
            }

            {   // New Item Panel
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 50, 10, 35, 45, 45, 45, false)]);
                SetP2Panel0();
            }

            {   // Items Panel
                ActivityPanel.Controls.Add(panel[InitPanel(1, 300, 170, 10, 100, 45, 45, 45, true)]);
                SetP2Panel1();
            }

            {   // Summary Panel
                ActivityPanel.Controls.Add(panel[InitPanel(2, 190, 180, 320, 100, 45, 45, 45, false)]);
                SetP2Panel2();
            }

            {   // Summary pannel2
                ActivityPanel.Controls.Add(panel[InitPanel(3, 300, 40, 10, 280, 45, 45, 45, false)]);
                SetP2Panel3();
            }

            {   // Cancel Button
                ActivityPanel.Controls.Add(button[InitButton(2, 30, 80, 320, 290, 12, "Cancel")]);
                button[2].Click += NewPurchase2Cancel;
            }

            {   // Next Button
                ActivityPanel.Controls.Add(button[InitButton(3, 30, 80, 420, 290, 12, "Next")]);
                button[3].Click += NewPurchase2Next;
            }
            combobox[0].Select();

        }                                  // New Purch Step 2
        private void NewPurchase2Next(object sender, EventArgs e)
        {
            int billno = 0;
            if (database.ItemNo == 0)
            {
                MessageBox.Show("Cant make bill with 0 items");
            }
            else
            {
                billno = database.CurrentBillno;
                //database.MakeBill(database.CurrentBillno, 's', database.ItemNo, database.BillTotal);
                if (database.MakeBill('p', datepicker[0].Value.Date.ToShortDateString(), Convert.ToInt32(textbox[2].Text), Convert.ToInt32(textbox[3].Text), Convert.ToInt32(textbox[4].Text), Convert.ToInt32(textbox[5].Text), "nil", "nil", "nil", Convert.ToInt32(numericupdown[2].Value)))
                {
                    DestroyNewPurchase2();
                    NewPurchase3(billno);
                }
                else
                {
                    MessageBox.Show("Unable to save bill due to connection problem with database", "Error");
                    DestroyNewPurchase2();
                    SetPurchase();
                }

            }

            //throw new NotImplementedException();
        }    // Next
        private void NewPurchase2Cancel(object sender, EventArgs e)
        {
            database.CancelBill(database.CurrentBillno, 'p');
            database.CurrentBillno = 0;
            DestroyNewPurchase2();
            SetHome();

            //throw new NotImplementedException();
        }  // Cancel
        private void SetP2Panel0()
        {
            {   // Item Name
                panel[0].Controls.Add(combobox[InitComboBox(0, 100, 5, 10, 10)]);
                combobox[0].Items.AddRange(database.GetItems('p'));
            }

            {   //  Qty Label
                panel[0].Controls.Add(label[InitLabel(10, 120, 10, 40, "Qty :")]);
            }

            {   // Item Qty
                numericupdown[0] = new NumericUpDown();
                numericupdown[0].Font = font10;
                point.X = 170;
                point.Y = 10;
                numericupdown[0].Location = point;
                numericupdown[0].Width = 50;
                numericupdown[0].Maximum = 9999;
                numericupdown[0].Enter += SelectNUD0Text2;
                panel[0].Controls.Add(numericupdown[0]);
            }

            {   //  Price Label
                panel[0].Controls.Add(label[InitLabel(10, 230, 10, 50, "Price")]);
            }

            {   //  Item Price
                numericupdown[1] = new NumericUpDown();
                numericupdown[1].Font = font10;
                point.X = 290;
                point.Y = 10;
                numericupdown[1].Location = point;
                numericupdown[1].Width = 50;
                numericupdown[1].Maximum = 10000;
                numericupdown[1].Increment = 10;
                numericupdown[0].Enter += SelectNUD1Text2;
                panel[0].Controls.Add(numericupdown[1]);
            }

            {   // Add Button
                panel[0].Controls.Add(button[InitButton(0, 30, 35, 400, 10, 12, "+")]);
                button[0].BackColor = Color.Silver;
                button[0].FlatAppearance.BorderColor = Color.DarkGray;
                button[0].FlatStyle = FlatStyle.Popup;
                button[0].ForeColor = Color.Green;
                button[0].Click += P2AddItem;
            }

            {   // Cancel Button
                panel[0].Controls.Add(button[InitButton(1, 30, 35, 450, 10, 12, "x")]);
                button[1].BackColor = Color.Silver;
                button[1].FlatAppearance.BorderColor = Color.DarkGray;
                button[1].FlatStyle = FlatStyle.Popup;
                button[1].ForeColor = Color.Red;
                button[1].Click += P2NICancel;

            }


        }                               // New Item Panel
        private void P2AddItem(object sender, EventArgs e)
        {

            if (database.ItemNo >= 20)
            {
                MessageBox.Show("Cant have more than 20 items in single bill");
                ClearP2NewItemControls();
            }

            else if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Select item from list ");
                combobox[0].DroppedDown = true;
            }
            else if (Convert.ToInt32(numericupdown[0].Value) == 0)
            {
                MessageBox.Show("Qty cant be 0");
                numericupdown[0].Select();
            }
            else if (Convert.ToInt32(numericupdown[1].Value) == 0)
            {
                MessageBox.Show("Price cant be 0");
                numericupdown[1].Select();
            }
            else if (database.AddItemToBill(database.CurrentBillno, database.GetItemId(combobox[0].Text, 'p'), Convert.ToInt32(numericupdown[0].Value), Convert.ToInt32(numericupdown[1].Value), 'p'))
            {
                panel[1].VerticalScroll.Value = 0;
                panel[1].Controls.Add(label[InitLabel(10, 3, 25 + (database.ItemNo * 25), 27, (database.ItemNo + 1).ToString())]);
                panel[1].Controls.Add(label[InitLabel(10, 35, 25 + (database.ItemNo * 25), 75, combobox[0].Text.ToString())]);
                label[LabelCount - 1].MouseEnter += ShowAddTooltip;
                panel[1].Controls.Add(label[InitLabel(10, 115, 25 + (database.ItemNo * 25), 55, Convert.ToInt32(numericupdown[0].Value).ToString())]);
                panel[1].Controls.Add(label[InitLabel(10, 175, 25 + (database.ItemNo * 25), 55, Convert.ToInt32(numericupdown[1].Value).ToString())]);
                panel[1].Controls.Add(label[InitLabel(10, 235, 25 + (database.ItemNo * 25), 55, (Convert.ToInt32(numericupdown[0].Value) * Convert.ToInt32(numericupdown[1].Value)).ToString())]);


                database.ItemNo++;
                database.BillTotal += Convert.ToInt32(numericupdown[0].Value) * Convert.ToInt32(numericupdown[1].Value);
                ClearP2NewItemControls();
                UpdateP2Panel2();
            }
            else
            {
                MessageBox.Show(database.LoginError.ToString());
            }


            //throw new NotImplementedException();
        }       // Add New Item (+ click)
        private void SetP2Panel1()
        {
            {       // Sr.                
                panel[1].Controls.Add(label[InitLabel(10, 3, 2, 27, "Sr.")]);
            }   // 3-30

            {       // Name
                panel[1].Controls.Add(label[InitLabel(10, 35, 2, 75, "Name")]);
            }   // 35-110

            {       // Qty
                panel[1].Controls.Add(label[InitLabel(10, 115, 2, 55, "Qty.")]);
            }   // 115-170

            {       // Price
                panel[1].Controls.Add(label[InitLabel(10, 175, 2, 55, "Price")]);
            }   // 175-210

            {       // Total
                panel[1].Controls.Add(label[InitLabel(10, 235, 2, 55, "Total")]);
            }   // 235-270



        }                               // All Items Panel
        private void SetP2Panel2()
        {
            panel[2].Controls.Add(label[InitLabel(10, 5, 5, 80, "Items")]);
            panel[2].Controls.Add(textbox[InitTextBox(0, 50, 10, 100, 5)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 35, 80, "iTotal")]);
            panel[2].Controls.Add(textbox[InitTextBox(1, 50, 10, 100, 35)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 65, 80, "Vat")]);
            panel[2].Controls.Add(textbox[InitTextBox(2, 50, 10, 100, 65)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 95, 80, "Postage")]);
            panel[2].Controls.Add(textbox[InitTextBox(3, 50, 10, 100, 95)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 125, 80, "Himali")]);
            panel[2].Controls.Add(textbox[InitTextBox(4, 50, 10, 100, 125)]);
            panel[2].Controls.Add(label[InitLabel(10, 5, 155, 80, "Total")]);
            panel[2].Controls.Add(textbox[InitTextBox(5, 50, 10, 100, 155)]);


            for (int i = 0; i < 6; i++)
            {
                if (i > 1 && i < 5)
                {
                    textbox[i].KeyUp += UpdateP2Panel2;
                    continue;
                }
                textbox[i].ForeColor = Color.White;
                textbox[i].Enabled = false;
                textbox[i].Text = "0";
            }
        }
        private void UpdateP2Panel2(object sender, KeyEventArgs e)
        {
            UpdateP2Panel2();
        }
        private void SetP2Panel3()
        {
            //panel[3].Controls.Add(label[InitLabel(10, 0, 5, 40, "PM")]);
            //panel[3].Controls.Add(textbox[InitTextBox(6, 45, 10, 40, 5)]);
            //panel[3].Controls.Add(label[InitLabel(10, 90, 5, 40, "GR")]);
            //panel[3].Controls.Add(textbox[InitTextBox(7, 45, 10, 135, 5)]);
            //panel[3].Controls.Add(label[InitLabel(10, 185, 5, 65, "Through")]);
            //panel[3].Controls.Add(textbox[InitTextBox(8, 50, 10, 250, 5)]);

        }                               // Summary Panel2  not needed in purch(copied sale)
        private void UpdateP2Panel2()
        {
            textbox[0].Text = database.ItemNo.ToString();
            textbox[1].Text = database.BillTotal.ToString();
            if (textbox[2].Text == "")
                textbox[2].Text = "0";
            if (textbox[3].Text == "")
                textbox[3].Text = "0";
            if (textbox[4].Text == "")
                textbox[4].Text = "0";

            textbox[5].Text = (database.BillTotal + Convert.ToInt32(textbox[2].Text) + Convert.ToInt32(textbox[3].Text) + Convert.ToInt32(textbox[4].Text)).ToString();
        }                            // Summary Panel
        private void P2NICancel(object sender, EventArgs e)
        {
            ClearS2NewItemControls();
            //throw new NotImplementedException();
        }      // New Item Cancel
        private void ClearP2NewItemControls()
        {
            combobox[0].Text = "";
            numericupdown[0].Value = 0;
            numericupdown[1].Value = 0;

            combobox[0].Select();
        }                    // Clear New Item Controls
        private void SelectNUD0Text2(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            numericupdown[0].Select(0, 4);           // Select its value
        }  // When NewSale2 NumerciUpdown[0] is selected 
        private void SelectNUD1Text2(object sender, EventArgs e)
        {
            numericupdown[1].Select(0, 5);
        }  // When NewSale2 NumerciUpdown[1] is selected 
        private void DestroyNewPurchase2()
        {
            ClearLabels();
            combobox[0].Dispose();
            numericupdown[0].Dispose();
            numericupdown[1].Dispose();
            datepicker[0].Dispose();
            for (int i = 0; i <= 5; i++)
            {
                textbox[i].Dispose();
                if (i > 3)
                    continue;
                button[i].Dispose();
            }
            panel[0].Dispose();
            panel[1].Dispose();
            panel[2].Dispose();
            panel[3].Dispose();
            ActivityPanel.Controls.Clear();
            database.CurrentBillno = 0;
            database.ItemNo = 0;
            database.BillTotal = 0;
        }                              // Clear Unwanted Controls


        //  //////////////////////////////////////////////////////////////////////////////////////

        private void NewPurchase3(int billno)
        {
            int e = database.MakePdfBill(billno, 'p');

            if (e == 1)
            {
                MessageBox.Show("Bill pdf created successfully");
                System.Diagnostics.Process.Start(Application.StartupPath + "/Bills/Purch/Billno" + billno + ".pdf");

            }
            else if (e == -1)
            {
                MessageBox.Show("Bills/Purch/Billno" + billno + ".pdf\nFile Already Open in other program");
            }
            else if (e == 0)
            {
                MessageBox.Show("Bill no." + billno + " Does not Exit in Database", "Database Error");
            }
            SetHome();

        }                        // Makes pdf of bill and opens in default program

        // ////////////////////////////////////////////////////////////////////////////////////////

        private void ViewPurchase1()
        {
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 270, 5, 5, 45, 45, 45, false)]);

            ActivityPanel.Controls.Add(panel[InitPanel(1, 500, 100, 5, 273, 45, 45, 45, false)]);

            panel[1].Controls.Add(button[InitButton(0, 25, 60, 15, 5, 11, "Prev.")]);
            button[0].Click += ViewPrev2;

            panel[1].Controls.Add(button[InitButton(1, 25, 60, 85, 5, 11, "Next")]);
            button[1].Click += ViewNext2;

            panel[1].Controls.Add(button[InitButton(2, 25, 100, 150, 5, 11, "ViewPdf")]);
            button[2].Click += ViewPdfP;

            panel[1].Controls.Add(button[InitButton(3, 25, 60, 300, 5, 11, "Delete")]);
            button[3].Click += DeleteBillP;

            panel[1].Controls.Add(button[InitButton(4, 25, 60, 400, 5, 11, "Exit")]);
            button[4].Click += ExitPV;

            database.ViewPageNo = 1;
            EnableNextPrev2();
            ShowBills2();

        }
        private void DeleteBillP(object sender, EventArgs e)
        {
            DestroyViewPurchase1();
            DeleteBill('p');
        }
        private void ExitPV(object sender, EventArgs e)
        {
            DestroyViewPurchase1();
            SetHome();
            //throw new NotImplementedException();
        }
        private void ViewPdfP(object sender, EventArgs e)
        {
            DestroyViewPurchase1();
            ViewPurchase2();
            //throw new NotImplementedException();
        }
        private void ShowBills2()
        {
            ClearLabels();
            panel[0].Controls.Clear();
            ShowBills1(0, 'p');
            EnableNextPrev2();
        }
        private void ViewNext2(object sender, EventArgs e)
        {
            database.ViewPageNo -= (database.ViewPageNo > 1) ? 1 : 0;
            ShowBills2();
            //throw new NotImplementedException();
        }
        private void ViewPrev2(object sender, EventArgs e)
        {
            database.ViewPageNo++;
            ShowBills2();
            //throw new NotImplementedException();
        }
        private void EnableNextPrev2()
        {
            button[1].Visible = true;
            if (database.ViewPageNo == 1)
                button[1].Visible = false;
            button[0].Visible = false;
            if (database.GetViewPages('p') > database.ViewPageNo)
                button[0].Visible = true;
        }
        private void DestroyViewPurchase1()
        {
            ClearLabels();
            for (int i = 0; i < 5; i++)
                button[i].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            panel[1].Controls.Clear();
            panel[1].Dispose();
            ActivityPanel.Controls.Clear();
            database.ViewPageNo = 0;
        }

        // /////////////////////////////////////////////////////////////////////////////////////


        private void ViewPurchase2()
        {
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 300, 5, 5, 45, 45, 45, false)]);

            panel[0].Controls.Add(label[InitLabel(11, 100, 20, 70, "Enter Bill No.")]);

            panel[0].Controls.Add(textbox[InitTextBox(0, 80, 12, 200, 20)]);

            panel[0].Controls.Add(button[InitButton(0, 30, 60, 300, 20, 11, "View Pdf")]);
            button[0].Click += Viewpdfp2;


            panel[0].Controls.Add(button[InitButton(1, 30, 60, 300, 80, 11, "Exit")]);
            button[1].Click += ExitP2;

            textbox[0].Select();

        }
        private void ExitP2(object sender, EventArgs e)
        {
            DestroyViewPurchase2();
            SetHome();
            //throw new NotImplementedException();
        }
        private void Viewpdfp2(object sender, EventArgs e)
        {
            int billno;
            try
            {
                billno = Convert.ToInt32(textbox[0].Text);
            }
            catch
            {
                MessageBox.Show("Bill No Entered not in correct Format");
                return;
            }
            if (billno != 0)
            {
                try
                {
                    if (database.MakePdfBill(billno, 'p') == 1)
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "/Bills/Purch/Billno" + billno.ToString() + ".pdf");
                    }
                    else
                    {
                        MessageBox.Show("Bill no " + billno.ToString() + " Does not Exit");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Cant open File");
                }
            }
            //throw new NotImplementedException();
        }
        private void DestroyViewPurchase2()
        {
            ClearLabels();
            for (int i = 0; i < 2; i++)
                button[i].Dispose();
            textbox[0].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
        }
        
        // ////////////////////////////////////////////////////////////////////////////////////////

        private void ViewSale1()
        {
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 270, 5, 5, 45, 45, 45, false)]);

            ActivityPanel.Controls.Add(panel[InitPanel(1, 500, 100, 5, 273, 45, 45, 45, false)]);

            panel[1].Controls.Add(button[InitButton(0, 25, 60, 15, 5, 11, "Prev.")]);
            button[0].Click += ViewPrev;
            
            panel[1].Controls.Add(button[InitButton(1, 25, 60, 85, 5, 11, "Next")]);
            button[1].Click += ViewNext;

            panel[1].Controls.Add(button[InitButton(2, 25, 100, 150, 5, 11, "ViewPdf")]);
            button[2].Click += ViewPdfS;

            panel[1].Controls.Add(button[InitButton(3, 25, 60, 300, 5, 11, "Delete")]);
            button[3].Click += DeleteBillS;

            panel[1].Controls.Add(button[InitButton(4, 25, 60, 400, 5, 11, "Exit")]);
            button[4].Click += ExitSV;

            database.ViewPageNo = 1;
            EnableNextPrev();
            ShowBills();

        }
        private void DeleteBillS(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DestroyViewSale1();
            DeleteBill('s');
        }
        private void ExitSV(object sender, EventArgs e)
        {
            DestroyViewSale1();
            SetHome();
            //throw new NotImplementedException();
        }
        private void ViewPdfS(object sender, EventArgs e)
        {
            DestroyViewSale1();
            ViewSale2();
            //throw new NotImplementedException();
        }
        private void ShowBills()
        {
            ClearLabels();
            panel[0].Controls.Clear();
            ShowBills1(0, 's');
            EnableNextPrev();
        }
        private void ViewNext(object sender, EventArgs e)
        {
            database.ViewPageNo -= (database.ViewPageNo > 1) ? 1 : 0;
            ShowBills();
            //throw new NotImplementedException();
        }
        private void ViewPrev(object sender, EventArgs e)
        {
            database.ViewPageNo++;
            ShowBills();
            //throw new NotImplementedException();
        }
        private void EnableNextPrev()
        {
            button[1].Visible = true;
            if (database.ViewPageNo == 1)
                button[1].Visible = false;
            button[0].Visible = false;
            if (database.GetViewPages('s') > database.ViewPageNo)
                button[0].Visible = true;
        }
        private void DestroyViewSale1()
        {
            ClearLabels();
            for (int i = 0; i < 5; i++)
                button[i].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            panel[1].Controls.Clear();
            panel[1].Dispose();
            ActivityPanel.Controls.Clear();
            database.ViewPageNo = 0;   
        }
        


        // /////////////////////////////////////////////////////////////////////////////////////


        private void ViewSale2()
        {
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 300, 5, 5, 45, 45, 45, false)]);

            panel[0].Controls.Add(label[InitLabel(11, 100, 20, 70, "Enter Bill No.")]);

            panel[0].Controls.Add(textbox[InitTextBox(0, 80, 12, 200, 20)]);

            panel[0].Controls.Add(button[InitButton(0, 30, 60, 300, 20, 11, "View Pdf")]);
            button[0].Click += Viewpdfs2;


            
            panel[0].Controls.Add(button[InitButton(1, 30, 60, 300, 80, 11, "Exit")]);
            button[1].Click += ExitS2;

            textbox[0].Select();

        }
        private void ExitS2(object sender, EventArgs e)
        {
            DestroyViewSale2();
            SetHome();
            //throw new NotImplementedException();
        }
        private void Viewpdfs2(object sender, EventArgs e)
        {
            int billno;
            try
            {
                billno = Convert.ToInt32(textbox[0].Text);
            }
            catch
            {
                MessageBox.Show("Bill No Entered not in correct Format");
                return;
            }
            if (billno != 0)
            {
                try
                {
                    if (database.MakePdfBill(billno, 's')==1)
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "/Bills/Sales/Billno" + billno.ToString() + ".pdf");
                    }
                    else
                    {
                        MessageBox.Show("Bill no " + billno.ToString() + " Does not Exit");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Cant open File");
                }
            }
            //throw new NotImplementedException();
        }
        private void DestroyViewSale2()
        {
            ClearLabels();
            for (int i = 0; i < 2; i++)
                button[i].Dispose();
            textbox[0].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
        }




        // ////////////////////////////////////////////////////////////////////////////////////////

        private void ShowBills1(int panelno, char Billtype)
        {
            int[] billnos = database.GetBillnos(Billtype);
            //MessageBox.Show("max :" + billnos[0] + " min :" + billnos[9]);
            int BillCount = 0;
            string[] BillInfo = new string[5];

            panel[0].Controls.Add(label[InitLabel(11, 5, 5, 50, "Bill")]);
            panel[0].Controls.Add(label[InitLabel(11, 60, 5, 120, "Party Name")]);
            panel[0].Controls.Add(label[InitLabel(11, 185, 5, 100, "Bill Date")]);
            panel[0].Controls.Add(label[InitLabel(11, 290, 5, 50, "Items")]);
            panel[0].Controls.Add(label[InitLabel(11, 345, 5, 75, "iTotal")]);
            panel[0].Controls.Add(label[InitLabel(11, 425, 5, 75, "Total")]);

            int InitY=30, GapY=20;
            while (billnos[BillCount] > 0)
            {
                BillInfo = database.GetBillInfo(billnos[BillCount], Billtype);
                BillInfo[3] = database.PartyInfo(Convert.ToInt32(BillInfo[3]))[1].ToString();

                panel[0].Controls.Add(label[InitLabel(11, 5, InitY + (GapY * BillCount), 50, billnos[BillCount].ToString())]);
                panel[0].Controls.Add(label[InitLabel(11, 60, InitY + (GapY * BillCount), 120, BillInfo[3])]);
                panel[0].Controls.Add(label[InitLabel(11, 185, InitY + (GapY * BillCount), 100, BillInfo[0])]);
                panel[0].Controls.Add(label[InitLabel(11, 290, InitY + (GapY * BillCount), 50, BillInfo[1])]);
                panel[0].Controls.Add(label[InitLabel(11, 345, InitY + (GapY * BillCount), 75, BillInfo[2])]);
                panel[0].Controls.Add(label[InitLabel(11, 425, InitY + (GapY * BillCount), 75, BillInfo[4])]);



                if (BillCount == 9)
                    break;
                BillCount++;
            }


        }


        // ///////////////////////////////////////////////////////////////////////////////////////

        private void NewOrder1()
        {
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 50, 5, 5, 45, 45, 45, false)]);
            {   // New Item Panel
                panel[0].Controls.Add(label[InitLabel(11, 15, 5, 80, "Item Name")]);
                panel[0].Controls.Add(textbox[InitTextBox(0, 100, 11, 95, 5)]);
                panel[0].Controls.Add(label[InitLabel(11, 215, 5, 50, "Qty:")]);
                panel[0].Controls.Add(textbox[InitTextBox(1, 40, 11, 275, 5)]); ;
                panel[0].Controls.Add(button[InitButton(0, 28, 60, 325, 5, 11, "Add")]);
                button[0].Click += NO1add;
                panel[0].Controls.Add(button[InitButton(1, 28, 70, 395, 5, 11, "Cancel")]);
                button[1].Click += NO1Cncl;

            }
            ActivityPanel.Controls.Add(panel[InitPanel(1, 270, 200, 5, 60, 45, 45, 45, true)]);
            {// Items
                panel[1].Controls.Add(label[InitLabel(11, 5, 5, 50, "Sr no.")]);
                panel[1].Controls.Add(label[InitLabel(11, 60, 5, 100, "Item Name")]);
                panel[1].Controls.Add(label[InitLabel(11, 170, 5, 100, "Qty")]);
            }
            ActivityPanel.Controls.Add(panel[InitPanel(2, 220, 200, 280, 60, 45, 45, 45, false)]);
            {
                panel[2].Controls.Add(label[InitLabel(11, 5, 5, 100, "Party Name")]);
                panel[2].Controls.Add(textbox[InitTextBox(2, 80, 11, 110, 5)]);
                panel[2].Controls.Add(label[InitLabel(11, 5, 45, 100, "Reminder Date")]);
                panel[2].Controls.Add(datepicker[InitDatePicker(0, 110, 45, 80)]);
                panel[2].Controls.Add(label[InitLabel(11, 5, 85, 100, "Delivery Date")]);
                panel[2].Controls.Add(datepicker[InitDatePicker(1, 110, 85, 80)]);
                datepicker[0].MinDate = DateTime.Today.Date;
                datepicker[1].MinDate = DateTime.Today.Date;


            }
            ActivityPanel.Controls.Add(panel[InitPanel(3, 500, 50, 5, 265, 45, 45, 45, false)]);
            {// Save Cancel
                panel[3].Controls.Add(button[InitButton(2, 30, 70, 300, 10, 11, "Cancel")]);
                button[2].Click += NO1Cancel;

                panel[3].Controls.Add(button[InitButton(3, 30, 70, 380, 10, 11, "Save")]);
                button[3].Click += NO1Save;
            }

            Pid = database.AddOrder();
            database.ItemNo = 0;
        }
        private void NO1Cncl(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ClearItemPnl();
        }
        private void NO1add(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (textbox[0].Text == "")
            {
                MessageBox.Show("Pls Enter Item name");
            }
            else if (textbox[1].Text == "")
            {
                MessageBox.Show("Pls Enter qty");
            }
            else
            {
                if (database.AddOrderItem(Pid, textbox[0].Text, Convert.ToInt32(textbox[1].Text)))
                {
                    panel[1].Controls.Add(label[InitLabel(11, 5, 25 + (database.ItemNo * 20), 50, (database.ItemNo + 1).ToString())]);
                    panel[1].Controls.Add(label[InitLabel(11, 60, 25 + (database.ItemNo * 20), 100, textbox[0].Text)]);
                    panel[1].Controls.Add(label[InitLabel(11, 170, 25 + (database.ItemNo * 20), 100, textbox[1].Text)]);
                }
                else
                {
                    MessageBox.Show("item not added");
                }
                database.ItemNo++;
                ClearItemPnl();
            }
            //database.AddOrderItem(Pid,)
        }
        private void ClearItemPnl()
        {
            textbox[0].Text = "";
            textbox[1].Text = "";

        }
        private void NO1Save(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (database.SaveOrder(Pid, database.ItemNo, datepicker[0].Value.Date.ToShortDateString(), datepicker[1].Value.Date.ToShortDateString(),textbox[2].Text))
            {
                MessageBox.Show("Order Saved Successfully");
            }
            DestroyNewOrder1();
            SetHome();
        }
        private void NO1Cancel(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            database.DeleteOrder(Pid);
            DestroyNewOrder1();
            SetHome();
        }
        private void DestroyNewOrder1()
        {
            Pid = 0;
            database.ItemNo = 0;
            ClearLabels();
            for (int i = 0; i <= 3; i++)
            {
                panel[i].Controls.Clear();
                panel[1].Dispose();
                button[i].Dispose();
                if (i == 3)
                    break;
                textbox[i].Dispose();
            }
            datepicker[0].Dispose();
            datepicker[1].Dispose();
            

        }

        // ////////////////////////////////////////////////////////////////////////////////////////

        private void OrderView1()
        {
            string[,] Orders = database.GetOrders();
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 250, 5, 5, 45, 45, 45, true)]);
            {   // View All
                {
                    panel[0].Controls.Add(label[InitLabel(11, 5, 5, 50, "Sr No.")]);
                    panel[0].Controls.Add(label[InitLabel(11, 60, 5, 90, "Date")]);
                    panel[0].Controls.Add(label[InitLabel(11, 155, 5, 90, "Rem Date")]);
                    panel[0].Controls.Add(label[InitLabel(11, 250, 5, 90, "Del Date")]);
                    panel[0].Controls.Add(label[InitLabel(11, 350, 5, 60, "Items")]);
                    panel[0].Controls.Add(label[InitLabel(11, 420, 5, 100, "Party Name")]);

                }
                int y = 25;
                for (int i = 0; i < Convert.ToInt32(Orders[0, 0]) - 1; i++)
                {
                    panel[0].Controls.Add(label[InitLabel(11, 5, y, 50, (i + 1).ToString())]);
                    panel[0].Controls.Add(label[InitLabel(11, 60, y, 90, Orders[0, i + 1])]);
                    panel[0].Controls.Add(label[InitLabel(11, 155, y, 90, Orders[2, i + 1])]);
                    panel[0].Controls.Add(label[InitLabel(11, 250, y, 90, Orders[1, i + 1])]);
                    panel[0].Controls.Add(label[InitLabel(11, 350, y, 60, Orders[3, i + 1])]);
                    panel[0].Controls.Add(label[InitLabel(11, 420, y, 100, Orders[4, i + 1])]);
                    y += 20;
                    {
                        string[,] Itemdetail = database.GetOrderItems(Convert.ToInt32(Orders[5, i + 1]), Convert.ToInt32(Orders[3, i + 1]));
                        for (int j = 0; j < Convert.ToInt32(Orders[3, i + 1]); j++)
                        {
                            panel[0].Controls.Add(label[InitLabel(11, 30, y, 100, Itemdetail[0, j])]);
                            panel[0].Controls.Add(label[InitLabel(11, 150, y, 100, Itemdetail[1, j])]);
                            y += 20;
                        }
                    }
                }
            }
            ActivityPanel.Controls.Add(panel[InitPanel(1, 500, 100, 5, 260, 45, 45, 45, false)]);
            {// panel for exit button
                panel[1].Controls.Add(button[InitButton(0, 30, 60, 400, 5, 11, "Exit")]);
                button[0].Click += ExitViewOrder;
            }
        }

        private void ExitViewOrder(object sender, EventArgs e)
        {
            DestroyOrderView();
            SetHome();
            //throw new NotImplementedException();
        }
        private void DestroyOrderView()
        {
            ClearLabels();
            button[0].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            panel[1].Controls.Clear();
            panel[1].Dispose();
            ActivityPanel.Controls.Clear();

        }
        
        // ////////////////////////////////////////////////////////////////////////////////////////

        private void PaySale1()
        {
            
            SelectPartyPanel('r');
            combobox[0].SelectedIndexChanged += PaySale1PartySelected;
            button[1].Click += PaySale1Next;
            button[0].Click += PaySale1Cancel;
            combobox[0].Select();
              
        }
        private void PaySale1Cancel(object sender, EventArgs e)
        {
            DestroyPaySale1();
            SetHome();

            //throw new NotImplementedException();
        }
        private void PaySale1Next(object sender, EventArgs e)
        {
            try
            {
                tooltip.Dispose();
            }
            catch { }
            if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Party");
                combobox[0].DroppedDown = true;
            }
            else
            {
                DestroyPaySale1();
                PaySale2();
                //newsale = new NewSale();
                //newsale.PartyId=
            }
            //throw new NotImplementedException();
        }
        private void PaySale1PartySelected(object sender, EventArgs e)
        {
            ToolTipPanelno = 1;
            string[] info = database.PartyInfo(combobox[0].SelectedIndex, 's');
            Pid = Convert.ToInt32(info[0]);
            panel[1].Controls.Clear();
            panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, info[1])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, info[3])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 90, 300, info[4])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;



            //newsale.Reset();
            //newsale.PInfo=database.PartyInfo(combobox[0].SelectedIndex, 's');
            //newsale.PartyId = Convert.ToInt32(newsale.PInfo[0]);
            //panel[1].Controls.Clear();
            //panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, newsale.PInfo[1])]);
            //panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, newsale.PInfo[3])]);
            //panel[1].Controls.Add(label[InitLabel(12,5,90,newsale.PInfo[4])]);
            ////throw new NotImplementedException();
        }
        private void DestroyPaySale1()
        {
            ClearLabels();
            combobox[0].Dispose();
            try
            {
                tooltip.Dispose();
            }
            catch { }
            panel[1].Controls.Clear();
            panel[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
            ToolTipPanelno = 0;

        }



        // /////////////////////////////////////////////////////////////////////////////////////////////

        private void PaySale2()
        {
            string[] info = database.PartyInfo(Pid);
            string[,] Details = database.GetPayDetails('s', Pid);

            ActivityPanel.Controls.Add(label[InitLabel(11, 5, 5, info[1])]);
            ActivityPanel.Controls.Add(label[InitLabel(11, 200, 5, 250, "Bal :" + Details[1, 0])]);

            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 200, 5, 35, 45, 45, 45, true)]);
            panel[0].Controls.Add(label[InitLabel(11, 5, 3, 150, "Date")]);
            panel[0].Controls.Add(label[InitLabel(11, 160, 3, 150, "Bill")]);
            panel[0].Controls.Add(label[InitLabel(11, 320, 3, 150, "Paid")]);

            //MessageBox.Show(Convert.ToInt32(Details[0, 0]).ToString());
            {   // details
                for (int i = 0; i < Convert.ToInt32(Details[0, 0]); i++)
                {
                    panel[0].Controls.Add(label[InitLabel(11, 5, 23 + (i * 20), 150, Details[0, i + 1])]);
                    if (Convert.ToInt32(Details[1, i + 1]) == 1)
                        panel[0].Controls.Add(label[InitLabel(11, 160, 23 + (i * 20), 150, Details[2, i + 1])]);
                    else
                        panel[0].Controls.Add(label[InitLabel(11, 320, 23 + (i * 20), 150, Details[2, i + 1])]);

                }
            }

            ActivityPanel.Controls.Add(panel[InitPanel(1, 500, 100, 5, 250, 45, 45, 45, false)]);
            {// Add pay

                panel[1].Controls.Add(button[InitButton(0, 30, 60, 20, 5, 11, "Add Pay")]);
                button[0].Click += Pays;
                panel[1].Controls.Add(button[InitButton(1, 30, 60, 100, 5, 11, "Exit")]);
                button[1].Click += Paysexit;
            }

        }
        private void Paysexit(object sender, EventArgs e)
        {
            DestroyPaySale2();
            SetHome();
            //throw new NotImplementedException();
        }
        private void Pays(object sender, EventArgs e)
        {
            int party = Pid;
            DestroyPaySale2();
            PaySale3(party);
            //throw new NotImplementedException();
        }
        private void DestroyPaySale2()
        {
            ClearLabels();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            panel[1].Controls.Clear();
            panel[1].Dispose();
            Pid = 0;
            ActivityPanel.Controls.Clear();
        }


        // /////////////////////////////////////////////////////////////////////////////////////////////
        private void PaySale3(int party)
        {
            Pid = party;
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 100, 5, 5, 45, 45, 45, false)]);
            {
                panel[0].Controls.Add(label[InitLabel(11, 80, 20, 80, "Amount :")]);
                panel[0].Controls.Add(textbox[InitTextBox(0, 100, 11, 170, 20)]);
                panel[0].Controls.Add(button[InitButton(0, 25, 75, 280, 20, 11, "Add")]);
                button[0].Click += AddPay;
                panel[0].Controls.Add(button[InitButton(1, 25, 75, 365, 20, 11, "Cancel")]);
                button[1].Click += CancelPayS;
            }
            textbox[0].Select();

        }
        private void CancelPayS(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DestroyPaySale3();
            SetHome();
        }
        private void AddPay(object sender, EventArgs e)
        {
            if (textbox[0].Text == "")
            {
                MessageBox.Show("Enter Amount :");

            }
            else if (Convert.ToInt32(textbox[0].Text) > 0)
            {
                if (database.AddPay('s', Pid, Convert.ToInt32(textbox[0].Text)))
                {
                    MessageBox.Show("Payment Added Successfully");
                }
            }
            else
            {
                MessageBox.Show("Enter amount correctly");
            }
            DestroyPaySale3();
            SetHome();
            //throw new NotImplementedException();
        }
        private void DestroyPaySale3()
        {
            Pid = 0;
            button[0].Dispose();
            button[1].Dispose();
            textbox[0].Dispose();
            ClearLabels();
            panel[0].Controls.Clear();
            panel[0].Dispose();
        }

        // ////////////////////////////////////////////////////////////////////////////////////////

        private void PayPurchase1()
        {

            SelectPartyPanel('o');
            combobox[0].SelectedIndexChanged += PaySale1PartySelected;
            button[1].Click += PayPurchase1Next;
            button[0].Click += PayPurchase1Cancel;
            combobox[0].Select();

        }
        private void PayPurchase1Cancel(object sender, EventArgs e)
        {
            DestroyPayPurchase1();
            SetHome();

            //throw new NotImplementedException();
        }
        private void PayPurchase1Next(object sender, EventArgs e)
        {
            try
            {
                tooltip.Dispose();
            }
            catch { }
            if (combobox[0].SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Party");
                combobox[0].DroppedDown = true;
            }
            else
            {
                DestroyPaySale1();
                PayPurchase2();
                //newsale = new NewSale();
                //newsale.PartyId=
            }
            //throw new NotImplementedException();
        }
        private void PayPurchase1PartySelected(object sender, EventArgs e)
        {
            ToolTipPanelno = 1;
            string[] info = database.PartyInfo(combobox[0].SelectedIndex, 'p');
            Pid = Convert.ToInt32(info[0]);
            panel[1].Controls.Clear();
            panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, info[1])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, info[3])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;
            panel[1].Controls.Add(label[InitLabel(12, 5, 90, 300, info[4])]);
            label[LabelCount - 1].MouseEnter += ShowAddTooltip;



            //newsale.Reset();
            //newsale.PInfo=database.PartyInfo(combobox[0].SelectedIndex, 's');
            //newsale.PartyId = Convert.ToInt32(newsale.PInfo[0]);
            //panel[1].Controls.Clear();
            //panel[1].Controls.Add(label[InitLabel(12, 5, 5, 300, newsale.PInfo[1])]);
            //panel[1].Controls.Add(label[InitLabel(12, 5, 45, 300, newsale.PInfo[3])]);
            //panel[1].Controls.Add(label[InitLabel(12,5,90,newsale.PInfo[4])]);
            ////throw new NotImplementedException();
        }
        private void DestroyPayPurchase1()
        {
            ClearLabels();
            combobox[0].Dispose();
            try
            {
                tooltip.Dispose();
            }
            catch { }
            panel[1].Controls.Clear();
            panel[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
            ToolTipPanelno = 0;

        }



        // /////////////////////////////////////////////////////////////////////////////////////////////

        private void PayPurchase2()
        {
            string[] info = database.PartyInfo(Pid);
            string[,] Details = database.GetPayDetails('p', Pid);

            ActivityPanel.Controls.Add(label[InitLabel(11, 5, 5, info[1])]);
            ActivityPanel.Controls.Add(label[InitLabel(11, 200, 5, 250, "Bal :" + Details[1, 0])]);

            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 200, 5, 35, 45, 45, 45, true)]);
            panel[0].Controls.Add(label[InitLabel(11, 5, 3, 150, "Date")]);
            panel[0].Controls.Add(label[InitLabel(11, 160, 3, 150, "Bill")]);
            panel[0].Controls.Add(label[InitLabel(11, 320, 3, 150, "Paid")]);

            //MessageBox.Show(Convert.ToInt32(Details[0, 0]).ToString());
            {   // details
                for (int i = 0; i < Convert.ToInt32(Details[0, 0]); i++)
                {
                    panel[0].Controls.Add(label[InitLabel(11, 5, 23 + (i * 20), 150, Details[0, i + 1])]);
                    if (Convert.ToInt32(Details[1, i + 1]) == 1)
                        panel[0].Controls.Add(label[InitLabel(11, 160, 23 + (i * 20), 150, Details[2, i + 1])]);
                    else
                        panel[0].Controls.Add(label[InitLabel(11, 320, 23 + (i * 20), 150, Details[2, i + 1])]);

                }
            }

            ActivityPanel.Controls.Add(panel[InitPanel(1, 500, 100, 5, 250, 45, 45, 45, false)]);
            {// Add pay

                panel[1].Controls.Add(button[InitButton(0, 30, 60, 20, 5, 11, "Add Pay")]);
                button[0].Click += Payp;
                panel[1].Controls.Add(button[InitButton(1, 30, 60, 100, 5, 11, "Exit")]);
                button[1].Click += Paypexit;
            }

        }
        private void Paypexit(object sender, EventArgs e)
        {
            DestroyPayPurchase2();
            SetHome();
            //throw new NotImplementedException();
        }
        private void Payp(object sender, EventArgs e)
        {
            int party = Pid;
            DestroyPayPurchase2();
            PayPurchase3(party);
            //throw new NotImplementedException();
        }
        private void DestroyPayPurchase2()
        {
            ClearLabels();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            panel[1].Controls.Clear();
            panel[1].Dispose();
            Pid = 0;
            ActivityPanel.Controls.Clear();
        }


        // /////////////////////////////////////////////////////////////////////////////////////////////
        private void PayPurchase3(int party)
        {
            Pid = party;
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 100, 5, 5, 45, 45, 45, false)]);
            {
                panel[0].Controls.Add(label[InitLabel(11, 80, 20, 80, "Amount :")]);
                panel[0].Controls.Add(textbox[InitTextBox(0, 100, 11, 170, 20)]);
                panel[0].Controls.Add(button[InitButton(0, 25, 75, 280, 20, 11, "Add")]);
                button[0].Click += AddPayp;
                panel[0].Controls.Add(button[InitButton(1, 25, 75, 365, 20, 11, "Cancel")]);
                button[1].Click += CancelPayS;
            }
            textbox[0].Select();
        }
        private void CancelPayP(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DestroyPayPurchase3();
            SetHome();
        }
        private void AddPayp(object sender, EventArgs e)
        {
            if (textbox[0].Text == "")
            {
                MessageBox.Show("Enter Amount :");

            }
            else if (Convert.ToInt32(textbox[0].Text) > 0)
            {
                if (database.AddPay('p', Pid, Convert.ToInt32(textbox[0].Text)))
                {
                    MessageBox.Show("Payment Added Successfully");
                }
            }
            else
            {
                MessageBox.Show("Enter amount correctly");
            }
            DestroyPaySale3();
            SetHome();
            //throw new NotImplementedException();
        }
        private void DestroyPayPurchase3()
        {
            Pid = 0;
            button[0].Dispose();
            button[1].Dispose();
            textbox[0].Dispose();
            ClearLabels();
            panel[0].Controls.Clear();
            panel[0].Dispose();
        }

        // /////////////////////////////////////////////////////////////////////////////////////////////

        private void Settings1()
        {
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 200, 5, 5, 45, 45, 45, false)]);
            {
                panel[0].Controls.Add(button[InitButton(0, 30, 200, 20, 20, 11, "Change Password")]);
                button[0].Click += ChangePass;
                panel[0].Controls.Add(button[InitButton(1, 30, 80, 20, 100, 11, "Exit")]);
                button[1].Click += SetExit;
            }
        }

        private void SetExit(object sender, EventArgs e)
        {
            DestroySettings();
            SetHome();
            //throw new NotImplementedException();
        }
        private void ChangePass(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DestroySettings();
            ChangePassword(0);
        }
        private void DestroySettings()
        {
            button[0].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();

        }
        // /////////////////////////////////////////////////////////////////////////////////////////////

        private void ChangePassword(int k)
        {
            if (k == 0)
            {
                ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 250, 5, 5, 45, 45, 45, false)]);
                {   // confirm old

                    panel[0].Controls.Add(label[InitLabel(11, 20, 20, 120, "Enter Old Password")]);
                    
                    panel[0].Controls.Add(textbox[InitTextBox(0, 100, 11, 150, 20)]);
                    textbox[0].PasswordChar = '*';
                    panel[0].Controls.Add(label[InitLabel(11, 20, 60, 80, "Enter New Password")]);
                    panel[0].Controls.Add(textbox[InitTextBox(1, 100, 11, 150, 60)]);
                    textbox[1].PasswordChar = '*';
                    panel[0].Controls.Add(button[InitButton(0, 28, 80, 50, 110, 11, "Cancel")]);
                    button[0].Click += CancelChange;
                    panel[0].Controls.Add(button[InitButton(1, 28, 80, 140, 110, 11, "Change")]);
                    button[1].Click += ChangeChange;
                }
            }
        }
        private void ChangeChange(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int ret = database.ChangePassword(textbox[0].Text, textbox[1].Text);
            switch (ret)
            {
                case -2:
                    {
                        MessageBox.Show("Unable to update password", "Dtabase Error");
                    }
                    break;
                case -1:
                    {
                        MessageBox.Show("Old Password entered is wrong");
                    }
                    break;
                case 0:
                    {
                        MessageBox.Show("Unable to contact Database", "Database error");
                    }
                    break;
                case 1:
                    {
                        MessageBox.Show("Password Updated SuccessFully");
                    }
                    break;
            }
            SetHome();
        }
        private void CancelChange(object sender, EventArgs e)
        {
            DestroyChange();
            SetHome();
            //throw new NotImplementedException();
        }
        private void DestroyChange()
        {
            ClearLabels();
            textbox[0].Dispose();
            textbox[1].Dispose();
            button[0].Dispose();
            button[1].Dispose();
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////


        private void DeleteBill(char BillType)
        {
            Pid = (BillType == 's') ? 1 : 2;
            ActivityPanel.Controls.Add(label[InitLabel(11, 5, 5, 150, "Delete " + ((BillType == 's') ? "Sale" : "Purchase") + " Bill")]);
            ActivityPanel.Controls.Add(panel[InitPanel(0, 500, 200, 5, 20, 45, 45, 45, false)]);
            panel[0].Controls.Add(label[InitLabel(11, 20, 10, "Enter Bill No.")]);
            panel[0].Controls.Add(textbox[InitTextBox(0, 80, 11, 150, 10)]);
            panel[0].Controls.Add(button[InitButton(0, 30, 60, 260, 10, 11, "Delete")]);
            button[0].Click += Deletebilld;
            panel[0].Controls.Add(button[InitButton(1, 30, 60, 150, 60, 11, "Exit")]);
            button[1].Click += Exitd;
            textbox[0].Select();
        }
        private void Exitd(object sender, EventArgs e)
        {

            DestroyDeleteBill();
            SetHome();
        }
        private void Deletebilld(object sender, EventArgs e)
        {
            if (textbox[0].Text == "")
            {
                MessageBox.Show("Enter Bill No.");
            }
            else if (Convert.ToInt32(textbox[0].Text) > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete BillNo" + textbox[0].Text, "Delete Bill", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (database.CancelBill(Convert.ToInt32(textbox[0].Text), ((Pid == 1) ? 's' : 'p')))
                    {
                        MessageBox.Show("Bill No. " + textbox[0].Text + " Deleted Successfully");
                        try
                        {
                            System.IO.File.Delete(Application.StartupPath + "/Bills/" + ((Pid == 1) ? "Sales" : "Purch") + "/Billno" + Convert.ToInt32(textbox[0].Text) + ".pdf");
                        }
                        catch
                        { }
                        DestroyDeleteBill();
                        SetHome();
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete Bill");
                    }
                }
            }
            else
            {
                MessageBox.Show("Bill No. enetered in format");
            }
            
        }
        private void DestroyDeleteBill()
        {
            Pid = 0;
            ClearLabels();
            textbox[0].Dispose();
            button[0].Dispose();
            button[1].Dispose();
            panel[0].Controls.Clear();
            panel[0].Dispose();
            ActivityPanel.Controls.Clear();
        }

        // /////////////////////////////////////////////////////////////////////////////////////////////////
        private void CheckOrder()
        {
            OrderRem.Text = database.Orderstatus();
        }




        private void NewSale()
        {
            ShowActivityPannel(1);
        }
        private void ViewSale()
        {
            ShowActivityPannel(2);
        }
        private void PrintSale()
        {
            ShowActivityPannel(3);
        }
        private void NewPurch()
        {
            ShowActivityPannel(4);
        }
        private void PrintPurch()
        {
            ShowActivityPannel(6);
        }
        private void ViewPurch()
        {
            ShowActivityPannel(5);
        }
        private void NewOrder()
        {
            ShowActivityPannel(7);
        }
        private void ViewOrder()
        {
            ShowActivityPannel(8);
        }
        private void EditOrder()
        {
            ShowActivityPannel(9);
        }
        private void NewParty()
        {
            ShowActivityPannel(10);
        }
        private void EditParty()
        {
            ShowActivityPannel(12);
        }
        private void ViewParty()
        {
            ShowActivityPannel(11);
        }
        private void NewItem()
        {
            ShowActivityPannel(13);
        }
        private void EditItem()
        {
            ShowActivityPannel(15);
        }
        private void ViewItem()
        {
            ShowActivityPannel(14);
        }
        private void NewSalePay()
        {
            ShowActivityPannel(16);
        }
        private void NewPurchPay()
        {
            ShowActivityPannel(17);
        }


        private void HomeSale_Click(object sender, EventArgs e)
        {
            SetSale();
        }
        private void HomeOrder_Click(object sender, EventArgs e)
        {
            SetOrder();
        }
        private void HomePurchase_Click(object sender, EventArgs e)
        {
            SetPurchase();
        }
        private void HomeParty_Click(object sender, EventArgs e)
        {
            SetParty();

        }
        private void HomeItem_Click(object sender, EventArgs e)
        {
            SetItem();
        }
        private void HomeSettings_Click(object sender, EventArgs e)
        {
            ShowActivityPannel(18);
        }
        private void HomePayment_Click(object sender, EventArgs e)
        {
            SetPayment();
        }
        private void HomePanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void PaymentHome_Click(object sender, EventArgs e)
        {
            SetHome();
        }
        private void ItemHome_Click(object sender, EventArgs e)
        {
            SetHome();
        }
        private void ItemHome_Click_1(object sender, EventArgs e)
        {
            SetHome();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            SetHome();
        }
        private void HomeSalen_Click(object sender, EventArgs e)
        {
            NewSale();
        }
        private void HomeOrdern_Click(object sender, EventArgs e)
        {
            NewOrder();
        }
        private void HomePurchasen_Click(object sender, EventArgs e)
        {
            NewPurch();
        }
        private void HomePartyn_Click(object sender, EventArgs e)
        {
            NewParty();
        }
        private void HomeItemn_Click(object sender, EventArgs e)
        {
            NewItem();
        }
        private void SaleHome_Click(object sender, EventArgs e)
        {
            SetHome();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SetHome();
        }
        private void PurchaseNew_Click(object sender, EventArgs e)
        {
            NewPurch();
        }
        private void PurchasePrint_Click(object sender, EventArgs e)
        {
            PrintPurch();
        }
        private void PurchaseView_Click(object sender, EventArgs e)
        {
            ViewPurch();
        }
        private void OrderHome_Click(object sender, EventArgs e)
        {
            
            SetHome();
        }
        private void SaleNew_Click(object sender, EventArgs e)
        {
            NewSale();
        }
        private void SaleView_Click(object sender, EventArgs e)
        {
            ViewSale();
        }
        private void SalePrint_Click(object sender, EventArgs e)
        {
            PrintSale();
        }
        private void PurchasePanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void OderNew_Click(object sender, EventArgs e)
        {
            NewOrder();
        }
        private void Oderp_Click(object sender, EventArgs e)
        {
            EditOrder();
        }
        private void Orderview_Click(object sender, EventArgs e)
        {
            ViewOrder();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            NewParty();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            EditParty();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ViewParty();
        }
        private void ItemsAddNew_Click(object sender, EventArgs e)
        {
            NewItem();

        }
        private void ItemEdit_Click(object sender, EventArgs e)
        {
            EditItem();
        }
        private void ItemView_Click(object sender, EventArgs e)
        {
            ViewItem();
        }
        private void PaymentSale_Click(object sender, EventArgs e)
        {
            NewSalePay();
        }
        private void PaymentPurchase_Click(object sender, EventArgs e)
        {
            NewPurchPay();
        }

        private void AbtBut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Software is Licensed to Chauhan International\nSoftware Made By Dishant Kumar\nContact : dishantdkc@gmail.com", "Sale Purchase Software");
            ShortCutText.Select();
        }
        
        private void LoginPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Signin();
            }
        }
        
        private void ShortCutText_KeyDown(object sender, KeyEventArgs e)
        {
         
            switch (e.KeyCode)
            {
                case Keys.S:
                    {
                        if (Panelcode == 1)
                        {
                            SetSale();
                        }
                        else if (Panelcode == 7)
                        {
                            // Payment Sale
                            ShowActivityPannel(16);
                        }
                    }
                    break;
                case Keys.U:
                    {
                        if (Panelcode == 1)
                        {
                            SetPurchase();
                        }
                        else if (Panelcode == 7)
                        {
                            // Payment Purchase
                            ShowActivityPannel(17);
                        }
                    }
                    break;
                case Keys.I:
                    {
                        if (Panelcode == 1)
                        {
                            SetItem();
                        }
                    }
                    break;
                case Keys.O:
                    {
                        if (Panelcode == 1)
                        {
                            SetOrder();
                        }
                    }
                    break;
                case Keys.Q:
                    {
                        if (Panelcode == 1)
                        {
                            SetParty();
                        }
                    }
                    break;
                case Keys.R:
                    {
                        if (Panelcode == 1)
                        {
                            SetPayment();
                        }
                    }
                    break;

                case Keys.H:
                    {
                        if (Panelcode > 1 && Panelcode <8)  //8 is activity panel
                        {
                            SetHome();
                        }
                    }
                    break;
                
                case Keys.Back:
                    {
                        //SetHome();
                    }
                    break;
                case Keys.A:
                    {
                        switch (Panelcode)
                        {
                            case 2:
                                {
                                    ShowActivityPannel(1);
                                }
                                break;
                            case 3:
                                {
                                    ShowActivityPannel(4);
                                }
                                break;
                            case 4:
                                {
                                    ShowActivityPannel(7);
                                }
                                break;
                            case 5:
                                {
                                    ShowActivityPannel(10);
                                }
                                break;
                            case 6:
                                {
                                    ShowActivityPannel(13);
                                }
                                break;
                        }

                    }
                    break;
                case Keys.V:
                    {
                        switch (Panelcode)
                        {
                            case 2:
                                {
                                    ShowActivityPannel(2);
                                }
                                break;
                            case 3:
                                {
                                    ShowActivityPannel(5);
                                }
                                break;
                            case 4:
                                {
                                    ShowActivityPannel(5);
                                }
                                break;
                            case 5:
                                {
                                    ShowActivityPannel(11);
                                }
                                break;
                            case 6:
                                {
                                    ShowActivityPannel(14);
                                }
                                break;


                        }

                    }
                    break;
                case Keys.P:
                    {
                        switch (Panelcode)
                        {
                            case 2:
                                {
                                    ShowActivityPannel(3);
                                }
                                break;
                            case 3:
                                {
                                    ShowActivityPannel(6);
                                }
                                break;
                           
                            
                        }

                    }
                    break;
                case Keys.E:
                    {
                        switch (Panelcode)
                        {
                            case 4:
                                {
                                    ShowActivityPannel(9);
                                }
                                break;
                            case 5:
                                {
                                    ShowActivityPannel(12);
                                }
                                break;
                            case 6:
                                {
                                    ShowActivityPannel(15);
                                }
                                break;
                        }


                    }
                    break;
            }
        }
        private void ShortCutText_KeyUp(object sender, KeyEventArgs e)
        {
            ShortCutText.Text = "";
        }
        private void ShortCutText_MouseHover(object sender, EventArgs e)
        {
            
            LoginTopText.Text = "ShortCut Keys";
            HomeSale.Text = "Sale(s)";
            HomePurchase.Text = "Purchase(u)";
            HomeItem.Text = "Item(i)";
            HomeParty.Text = "Party(q)";
            HomeOrder.Text = "Order(o)";
            HomeSettings.Text = "Settings(t)";
            HomePayment.Text = "Payment(r)";
            
            PartyNew.Text = "Add New Party(a)";
            PartyEdit.Text = "Edit Party(e)";
            PartyView.Text = "View All Party(v)";
            PartyHome.Text = "Home(h)";

            SaleNew.Text = "Add New(a)";
            SalePrint.Text = "Print(p)";
            SaleView.Text = "View All(v)";
            SaleHome.Text = "Home(h)";

            ItemNew.Text = "Add New(a)";
            ItemView.Text = "View All(v)";
            ItemEdit.Text = "Edit Item(e)";
            ItemHome.Text = "Home(h)";

            OrderNew.Text = "Add New(a)";
            OrderView.Text = "View All(v)";
            OrderHome.Text = "Home(h)";

            PurchaseNew.Text = "Add New(a)";
            PurchasePrint.Text = "Print(p)";
            PurchaseView.Text = "View All(v)";
            PurchaseHome.Text = "Home(h)";

            PaymentSale.Text = "Sale(s)";
            PaymentPurchase.Text = "Purchase(u)";
            

        }
        private void ShortCutText_MouseLeave(object sender, EventArgs e)
        {
            LoginTopText.Text = database.LoginError;
            HomeSale.Text = "Sale";
            HomePurchase.Text = "Purchase";
            HomeItem.Text = "Item";
            HomeParty.Text = "Party";
            HomeOrder.Text = "Order";
            HomeSettings.Text = "Settings";
            HomePayment.Text = "Payment";

            PartyNew.Text = "Add New Party";
            PartyEdit.Text = "Edit Party";
            PartyView.Text = "View All Party";
            PartyHome.Text = "Home";

            SaleNew.Text = "Add New";
            SalePrint.Text = "Print";
            SaleView.Text = "View All";
            SaleHome.Text = "Home";

            ItemNew.Text = "Add New";
            ItemView.Text = "View All";
            ItemEdit.Text = "Edit Item";
            ItemHome.Text = "Home";

            OrderNew.Text = "Add New";
            OrderView.Text = "View All";
            OrderHome.Text = "Home";

            PurchaseNew.Text = "Add New";
            PurchasePrint.Text = "Print";
            PurchaseView.Text = "View All";
            PurchaseHome.Text = "Home";

            PaymentSale.Text = "Sale";
            PaymentPurchase.Text = "Purchase";
        
        }


        
        
       
        
    }
}
