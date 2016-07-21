using EasyModbus;
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
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

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

        static int counter;
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

        

        /// <summary>
        /// Initializes a new instance of the <see cref="Window1"/> class.
        /// </summary>
        public Window1()
        {
            this.InitializeComponent();

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
            m_helix_viewport.Children.Add(mybox);

            //this.MyKUKA = KUKA_KR90;
            this.MyStaubli = Staubli_TX60;


            
            Main_Grid.DataContext = this;

            modbusClient = new ModbusClient("127.0.0.1", 502); 

            counter = 0;
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 2);
            //_timer.Start();

            _update_timer = new System.Windows.Threading.DispatcherTimer();
            _update_timer.Tick += new EventHandler(_update_timer_Tick);
            _update_timer.Interval = new TimeSpan(0, 0, 0,0,100);
           // _update_timer.Start();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            

            // animate_a1(-30, 5);
           // KUKA_A1.BeginAnimation(A1_angle, testanimation);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            switch (counter)
            {
                case 0 :
                    animate_a1(60, 2);
                    animate_a2(30, 2);
                    animate_a3(20, 2);
                     animate_a4(20, 2);
                     animate_a5(20, 2);
                     counter++;
                    break;
                case 1 : 
                    animate_a1(30, 2);
                    animate_a2(70, 2);
                    animate_a3(-40, 2);
                     animate_a4(80, 2);
                     animate_a5(80, 2);
                     
                    counter++;
                    break;
                case 2:
                    
                    animate_a1(-80, 2);
                    animate_a2(-30, 2);
                    animate_a3(-20, 2);
                     animate_a4(-70, 2);
                     animate_a5(-20, 2);
                     counter++;
                    break;
                case 3:
                    animate_a1(-20, 2);
                    animate_a2(30, 2);
                    animate_a3(-20, 2);
                     animate_a4(120, 2);
                     animate_a5(-100, 2);
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
                   //Ip-Address and Port of Modbus-TCP-Server
                modbusClient.Connect();

                //Read 10 Coils from Server, starting with address 10
                int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 6);
                move_a1(readHoldingRegisters[0]);
                move_a2(readHoldingRegisters[1]);
                move_a3(readHoldingRegisters[2]);
                move_a4(readHoldingRegisters[3]);
                move_a5(readHoldingRegisters[4]);
                
                modbusClient.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        
    }
}