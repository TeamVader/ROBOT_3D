﻿using EasyModbus;
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// <summary>
//   Interaction logic for Window1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using HelixToolkit.Wpf;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace ModelViewer
{
    /// <summary>
    /// Interaction logic for Window1.
    /// </summary>
    public partial class Window1
    {

        System.Windows.Threading.DispatcherTimer _timer;
        System.Windows.Threading.DispatcherTimer _update_timer;

        public ModbusClient modbusClient;
        public string ip_modbus ;
        public bool connected;
    
        static int counter;
        static int multiplier = 44;
        static int range = 32000;
        double real_value_a1;
        double real_value_a2;
        double real_value_a3;
        double real_value_a4;
        double real_value_a5;
        double real_value_a6;
        //the small box to find pints in the 3D World
        BoxVisual3D mybox;

        //Binding Model
        
        ModelImporter mi = new ModelImporter();

        #region Helper_Box_properties
        public double boxheigth
        {
            get { return mybox.Height; }
            set { mybox.Height = value; }
        }

        public double boxwidth
        {
            get { return mybox.Width; }
            set { mybox.Width = value; }
        }

        public double boxlength
        {
            get { return mybox.Length; }
            set { mybox.Length = value; }
        }


        public double boxX
        {
            get { return mybox.Center.X; }
            set { mybox.Center = new Point3D(value, mybox.Center.Y, mybox.Center.Z); }
        }


        public double boxY
        {
            get { return mybox.Center.Y; }
            set { mybox.Center = new Point3D(mybox.Center.X, value, mybox.Center.Z); }
        }

        public double boxZ
        {
            get { return mybox.Center.Z; }
            set { mybox.Center = new Point3D(mybox.Center.X, mybox.Center.Y, value); }
        }

        #endregion


        #region Window
        /// <summary>
        /// Initializes a new instance of the <see cref="Window1"/> class.
        /// </summary>
        public Window1()
        {
            this.InitializeComponent();


            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, this.OnMaximizeWindow, this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow, this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow, this.OnCanResizeWindow));

            #region KUKA INIT
            KUKA_KR90 = new Model3DGroup();
            
            KUKA_Base = mi.Load(@"Models/KUKA/KUKA_Base.3ds");
            KUKA_A1 = mi.Load(@"Models/KUKA/KUKA_A1.3ds");
            KUKA_A2 = mi.Load(@"Models/KUKA/KUKA_A2.3ds");
            KUKA_A3 = mi.Load(@"Models/KUKA/KUKA_A3.3ds");
            KUKA_A4 = mi.Load(@"Models/KUKA/KUKA_A4.3ds");
            KUKA_A5 = mi.Load(@"Models/KUKA/KUKA_A5.3ds");
            KUKA_A2_Counterbalance = mi.Load(@"Models/KUKA/KUKA_Counterbalancing_A2.3ds");
            KUKA_A2_Shaft = mi.Load(@"Models/KUKA/KUKA_Shaft_A2.3ds");

            KUKA_KR90.Children.Add(KUKA_Base);
            KUKA_Base.Children.Add(KUKA_A1);
            KUKA_A1.Children.Add(KUKA_A2);
            KUKA_A1.Children.Add(KUKA_A2_Counterbalance);
            /*************************************/
            KUKA_A2.Children.Add(KUKA_A2_Shaft);
            KUKA_A2.Children.Add(KUKA_A3);
            KUKA_A3.Children.Add(KUKA_A4);
            KUKA_A4.Children.Add(KUKA_A5);

            //rotate whole skeleton to have it upright
            RotateTransform3D myRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -90));
            myRotateTransform.CenterX = 0;
            myRotateTransform.CenterY = 0;
            myRotateTransform.CenterZ = 0;
            KUKA_KR90.Transform = myRotateTransform;
            #endregion
            //instanciate Helper box, uncomment to use it

            #region STAUBLI INIT
            Staubli_TX60 = new Model3DGroup();
            Staubli_Base = mi.Load(@"Models/STAUBLI/TX60_Base.3ds");
            Staubli_A1 = mi.Load(@"Models/STAUBLI/TX60_A1.3ds");
            Staubli_A2 = mi.Load(@"Models/STAUBLI/TX60_A2.3ds");
            Staubli_A3 = mi.Load(@"Models/STAUBLI/TX60_A3.3ds");
            Staubli_A4 = mi.Load(@"Models/STAUBLI/TX60_A4.3ds");
            Staubli_A5 = mi.Load(@"Models/STAUBLI/TX60_A5.3ds");
            Staubli_A6 = mi.Load(@"Models/STAUBLI/TX60_A6.3ds");


            Staubli_TX60.Children.Add(Staubli_Base);
            Staubli_Base.Children.Add(Staubli_A1);
            Staubli_A1.Children.Add(Staubli_A2);
            Staubli_A2.Children.Add (Staubli_A3);
            Staubli_A3.Children.Add( Staubli_A4);
            Staubli_A4.Children.Add(Staubli_A5);
            Staubli_A5.Children.Add(Staubli_A6);

            #endregion

            mybox = new BoxVisual3D();
            mybox.Height = 0.01;
            mybox.Width = 0.01;
            mybox.Length = 0.01;
            //m_helix_viewport.Children.Add(mybox);

            //this.MyKUKA = KUKA_KR90;
            this.MyStaubli = Staubli_TX60;

            connected = false;
            ip_modbus = "222.1.0.2";
            Main_Grid.DataContext = this;

            modbusClient = new ModbusClient(ip_modbus, 502); 

            counter = 0;
            _timer = new System.Windows.Threading.DispatcherTimer(DispatcherPriority.Render);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 2);
           // _timer.Start();

            _update_timer = new System.Windows.Threading.DispatcherTimer(DispatcherPriority.Render);
            _update_timer.Tick += new EventHandler(_update_timer_Tick);
            _update_timer.Interval = new TimeSpan(0, 0, 0,0,40);
            _update_timer.Start();
            
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            
            // animate_a1(-30, 5);
           // KUKA_A1.BeginAnimation(A1_angle, testanimation);
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
            Application.Current.Shutdown();
            
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
       

        #endregion

        void _timer_Tick(object sender, EventArgs e)
        {
            switch (counter)
            {
                case 0 :
                    Staubli_animate_a1(60, 2);
                    Staubli_animate_a2(30, 2);
                    Staubli_animate_a3(20, 2);
                    Staubli_animate_a4(20, 2);
                    Staubli_animate_a5(20, 2);
                    Staubli_animate_a6(10, 2);
                     counter++;
                    break;
                case 1 :
                    Staubli_animate_a1(30, 2);
                    Staubli_animate_a2(70, 2);
                    Staubli_animate_a3(-40, 2);
                    Staubli_animate_a4(80, 2);
                    Staubli_animate_a5(80, 2);
                    Staubli_animate_a6(70, 2);
                    counter++;
                    break;
                case 2:

                    Staubli_animate_a1(-80, 2);
                    Staubli_animate_a2(-30, 2);
                    Staubli_animate_a3(-20, 2);
                    Staubli_animate_a4(-70, 2);
                    Staubli_animate_a5(-20, 2);
                    Staubli_animate_a6(-20, 2);
                     counter++;
                    break;
                case 3:
                    Staubli_animate_a1(-20, 2);
                    Staubli_animate_a2(30, 2);
                    Staubli_animate_a3(-20, 2);
                    Staubli_animate_a4(120, 2);
                    Staubli_animate_a5(-100, 2);
                    Staubli_animate_a6(-100, 2);
                    counter++;
                    break;
                case 4:

                    counter = 5;
                    break;
            }
        }

        void _update_timer_Tick(object sender, EventArgs e)
        {
            try
            {

                if (connected == true)
                {
                   
                    //Ip-Address and Port of Modbus-TCP-Server
                    // 
                    modbusClient.Connect();
                    //Read 10 Coils from Server, starting with address 10
                    int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 6);
                    for (int i = 0; i < 6; i++)
                    {
                        if (readHoldingRegisters[i] < range/2)
                        {
                            switch(i)
                            {
                                case 0 :
                                Staubli_move_a1((double)readHoldingRegisters[i] / multiplier);
                                real_value_a1 = (double)readHoldingRegisters[i] / multiplier;
                                break;
                                case 1:
                                Staubli_move_a2((double)readHoldingRegisters[i] / multiplier);
                                real_value_a2 = (double)readHoldingRegisters[i] / multiplier;
                                break;
                                case 2:
                                Staubli_move_a3((double)readHoldingRegisters[i] / multiplier);
                                real_value_a3 = (double)readHoldingRegisters[i] / multiplier;
                                break;
                                case 3:
                                Staubli_move_a4((double)readHoldingRegisters[i] / multiplier);
                                real_value_a4 = (double)readHoldingRegisters[i] / multiplier;
                                break;
                                case 4:
                                Staubli_move_a5((double)readHoldingRegisters[i] / multiplier);
                                real_value_a5 = (double)readHoldingRegisters[i] / multiplier;
                                break;
                                case 5:
                                Staubli_move_a6((double)readHoldingRegisters[i] / multiplier);
                                real_value_a6 = (double)readHoldingRegisters[i] / multiplier;
                                break;

                            }
                        }
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    Staubli_move_a1((double)(readHoldingRegisters[i] - range) / multiplier);
                                    real_value_a1 = (double)(readHoldingRegisters[i] - range) / multiplier;
                                    break;
                                case 1:
                                    Staubli_move_a2((double)(readHoldingRegisters[i] - range) / multiplier);
                                    real_value_a2 = (double)(readHoldingRegisters[i] - range) / multiplier;
                                    break;
                                case 2:
                                    Staubli_move_a3((double)(readHoldingRegisters[i] - range) / multiplier);
                                    real_value_a3 = (double)(readHoldingRegisters[i] - range) / multiplier;
                                    break;
                                case 3:
                                    Staubli_move_a4((double)(readHoldingRegisters[i] - range) / multiplier);
                                    real_value_a4 = (double)(readHoldingRegisters[i] - range) / multiplier;
                                    break;
                                case 4:
                                    Staubli_move_a5((double)(readHoldingRegisters[i] - range) / multiplier);
                                    real_value_a5 = (double)(readHoldingRegisters[i] - range) / multiplier;
                                    break;
                                case 5:
                                    Staubli_move_a6((double)(readHoldingRegisters[i] - range) / multiplier);
                                    real_value_a6 = (double)(readHoldingRegisters[i] - range) / multiplier;
                                    break;

                            }
                            
                        }
                    }
                  
                     modbusClient.Disconnect();
                    //Add Visual output
                     ConsoleOutput.Items.Add("J1: " + real_value_a1.ToString("0.00") + " J2: " + real_value_a2.ToString("0.00") + " J3: " + real_value_a3.ToString("0.00") + " J4: " + real_value_a4.ToString("0.00") + " J5: " + real_value_a5.ToString("0.00") + " J6: " + real_value_a6.ToString("0.00"));
                    if (ConsoleOutput.Items.Count >= 15)
                    {
                        ConsoleOutput.Items.RemoveAt(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.TextConnection.Text = "Connection Refused to Server";
                this.Connected.BorderBrush = Brushes.Red;
                connected = false;
                //MessageBox.Show(ex.StackTrace);
            }

        }

        private void buttonconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (modbusClient.Available(500))
                {
                    ip_modbus = IPControl.Text;
                    ConsoleOutput.Items.Add("Try to Connect to IP : " + ip_modbus + " Port 502 " + DateTime.Now);
                    modbusClient = new ModbusClient(ip_modbus, 502);
                    modbusClient.Connect();
                    this.TextConnection.Text = "Connected to Server";
                    this.Connected.BorderBrush = Brushes.Green;
                    ConsoleOutput.Items.Add("Connected succesfull to IP : " + ip_modbus + " Port 502 ");
                    connected = true;
                }
                else
                {
                    this.TextConnection.Text = "Connection Refused to Server";
                    this.Connected.BorderBrush = Brushes.Red;
                    connected = false;
                }
              }
            catch (Exception ex)
            {
                this.TextConnection.Text = "Connection Refused to Server";
                this.Connected.BorderBrush = Brushes.Red;
                connected = false;
                //MessageBox.Show(ex.StackTrace);
            }
        }

        
        
    }
}