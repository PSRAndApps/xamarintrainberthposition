using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace TrainBerthPosition
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private int selectedClass = -1;
        private int berthNo = 0;
        private string displayText = "";
        private static readonly int MIN_SEAT_NUM = 1;
        private static readonly int MAX_2ND_LIMIT = 46;
        private static readonly int MAX_3RD_LIMIT = 64;
        private static readonly int MAX_GR_LIMIT = 81;
        private static readonly int MAX_SL_LIMIT = 72;
        private static readonly int MAX_2S_LIMIT = 108;
        private static readonly int MAX_2SJ_LIMIT = 106;
        private static readonly int MAX_EC_LIMIT = 56;
        private static readonly int MOD_2ND = 6;
        private static readonly int MOD_3RD = 8;
        private static readonly int MOD_GR = 9;
        private static readonly int MOD_SL = 8;
        private static readonly int MOD_EC = 4;
        private static readonly string AS = "Asile Seat";
        private static readonly string LB = "Lower Berth";
        private static readonly string MB = "Middle Berth";
        private static readonly string MS = "Middle Seat";
        private static readonly string UB = "Upper Berth";
        private static readonly string SLB = "Side Lower Berth";
        private static readonly string SMB = "Side Middle Berth";
        private static readonly string SUB = "Side Upper Berth";
        private static readonly string WS = "Window Seat";
        private static readonly string ERROR_MSG1 = "Please enter valid berth/seat number in between ";
        private static readonly string ERROR_MSG2 = " and ";
        private static readonly string INFO_MSG1 = "For the number ";
        private static readonly string INFO_MSG2 = ", you have got \n";
        private static readonly string INFO_MSG3 = ".";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Find_Button_Clicked(object sender, EventArgs e)
        {
            selectedClass = traintype.SelectedIndex;
            berthNo = Int32.Parse(seatno.Text);
            displayText = calculateBerth(selectedClass, berthNo);
            await DisplayAlert("Berth/Seat Position", displayText, "OK");

            // Clear entry field
            seatno.Text = "";
        }

        private string calculateBerth(int selectedClass, int berthNo)
        {
            var seatNo = -1;
            switch (selectedClass)
            {
                case 0:
                    if (berthNo == 45 || berthNo == 46)
                    {
                        return SecondAc(berthNo);
                    }
                    else
                    {
                        return SecondAc(checkClassSeat(berthNo, MAX_2ND_LIMIT, MOD_2ND));
                    }
                case 1:
                    return ThirdAc(checkClassSeat(berthNo, MAX_3RD_LIMIT, MOD_3RD));

                case 2:
                case 6:
                    return ExecutiveChairCar(checkClassSeat(berthNo, MAX_EC_LIMIT, MOD_EC));

                case 3:
                    return garibRath(checkClassSeat(berthNo, MAX_GR_LIMIT, MOD_GR));

                case 4:
                    return secondSeating(checkClassSeat(berthNo, MAX_2S_LIMIT, MOD_2ND));

                case 5:
                    if (berthNo > 0 && berthNo < 5)
                    {
                        if (berthNo == 4)
                        {
                            seatNo = berthNo + 1;
                        }
                        else
                        {
                            seatNo = berthNo;
                        }
                    }
                    else if (berthNo > 4 && berthNo < 106)
                    {
                        seatNo = berthNo - 5;
                    }
                    else if (berthNo == 106)
                    {
                        seatNo = MOD_2ND;
                    }
                    return secondSeatingJS(checkClassSeat(seatNo, MAX_2SJ_LIMIT, MOD_2ND));

                case 7:
                    return sleeperClass(checkClassSeat(berthNo, MAX_SL_LIMIT, MOD_SL));
            }
            return null;
        }

        private string ExecutiveChairCar(int pos)
        {
            if (pos != -1)
            {
                if (pos == 0 || pos == 1)
                {
                    return infoMsg(berthNo, WS);
                }
                else if (pos == 2 || pos == 3)
                {
                    return infoMsg(berthNo, AS);
                }
            }

            return errorMsg(MAX_EC_LIMIT);
        }

        private string sleeperClass(int pos)
        {
            if (pos != -1)
            {
                return ThirdAc(pos);
            }
            else
            {
                return errorMsg(MAX_SL_LIMIT);
            }
        }

        private string secondSeatingJS(int pos)
        {
            if (pos != -1)
            {
                return secondSeating(pos);
            }
            else
            {
                return errorMsg(MAX_2SJ_LIMIT);
            }
        }

        private string secondSeating(int pos)
        {
            switch (pos)
            {
                case -1:
                    return errorMsg(MAX_2S_LIMIT);

                case 0:
                case 1:
                    return infoMsg(berthNo, WS);

                case 2:
                case 5:
                    return infoMsg(berthNo, MS);

                case 3:
                case 4:
                    return infoMsg(berthNo, AS);

                default:
                    return errorMsg(MAX_2S_LIMIT);
            }
        }

        private string garibRath(int pos)
        {
            if (pos != -1)
            {
                if (pos == 1 || pos == 4)
                {
                    return infoMsg(berthNo, LB);
                }
                else if (pos == 2 || pos == 5)
                {
                    return infoMsg(berthNo, MB);
                }
                else if (pos == 3 || pos == 6)
                {
                    return infoMsg(berthNo, UB);
                }
                else if (pos == 7)
                {
                    return infoMsg(berthNo, SLB);
                }
                else if (pos == 8)
                {
                    return infoMsg(berthNo, SMB);
                }
                else if (pos == 0)
                {
                    return infoMsg(berthNo, SUB);
                }
            }

            return errorMsg(MAX_GR_LIMIT);
        }

        private string ThirdAc(int pos)
        {
            if (pos != -1)
            {
                if (pos == 1 || pos == 4)
                {
                    return infoMsg(berthNo, LB);
                }
                else if (pos == 2 || pos == 5)
                {
                    return infoMsg(berthNo, MB);
                }
                else if (pos == 3 || pos == 6)
                {
                    return infoMsg(berthNo, UB);
                }
                else if (pos == 7)
                {
                    return infoMsg(berthNo, SLB);
                }
                else if (pos == 0)
                {
                    return infoMsg(berthNo, SUB);
                }
            }

            return errorMsg(MAX_3RD_LIMIT);
        }

        private string SecondAc(int pos)
        {
            if (pos == -1)
            {
                return errorMsg(MAX_2ND_LIMIT);
            }
            else if (pos == 1 || pos == 3)
            {
                return infoMsg(berthNo, LB);
            }
            else if (pos == 2 || pos == 4)
            {
                return infoMsg(berthNo, UB);
            }
            else if (pos == 5)
            {
                return infoMsg(berthNo, SLB);
            }
            else if (pos == 0)
            {
                return infoMsg(berthNo, SUB);
            }
            else if (pos == 45)
            {
                return infoMsg(berthNo, LB);
            }
            else if (pos == 46)
            {
                return infoMsg(berthNo, UB);
            }

            return null;
        }

        private string errorMsg(int maxSeatNo)
        {
            return ERROR_MSG1 + MIN_SEAT_NUM + ERROR_MSG2 + maxSeatNo;
        }

        private string infoMsg(int berthNo, string pos)
        {
            return INFO_MSG1 + berthNo + INFO_MSG2 + pos + INFO_MSG3;
        }

        private int checkClassSeat(int berthNo, int limit, int modval)
        {
            if (berthNo >= MIN_SEAT_NUM && berthNo <= limit)
            {
                return berthNo % modval;
            }
            else
            {
                return -1;
            }
        }

        private void traintype_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedClass = traintype.SelectedIndex;
        }

        private async void About_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new About());
        }
        private async void Close_Button_Clicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Closing Application", "You are about to close the app, Do want to continue?", "Yes", "No");
            if (response)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
    }
}