using System;
using System.Collections.Generic;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeekControl : ContentView
	{
        //* Public Properties
        public DateTime DateSelected;
        public DateStringViewModel DateProp;

        //* Public Events
        public event EventHandler DataSelectedChanged;

        //* Private Properties
        private List<DayViewModel> dataList = new List<DayViewModel>();

        //* Constructors
        public WeekControl()
        {
            InitializeComponent();

            DateProp = new DateStringViewModel();
            setUpDateElements();
            fillDatesWithToday();
        }

        //* Public Methods
        public void ShiftDatesBackward()
        {
            DateTime firstDate = DateTime.Today;

            int year = dataList[0].Date.Year;
            string month = dataList[0].Month;
            int day = dataList[0].Date.Day;

            string concat = string.Format("{0}-{1}-{2}", year, month, day);

            DateTime.TryParseExact(concat, "yyyy-MMMM-d", null, DateTimeStyles.None, out firstDate);

            for (int i = dataList.Count - 1; i >= 0; i--)
            {
                firstDate = firstDate.AddDays(-1);
                changeBindingDate(i, firstDate);
            }
        }

        public void ShiftDatesForward()
        {
            DateTime lastDate = DateTime.Today;

            int year = dataList[dataList.Count - 1].Date.Year;
            string month = dataList[dataList.Count - 1].Month;
            int day = dataList[dataList.Count - 1].Date.Day;

            string concat = string.Format("{0}-{1}-{2}", year, month, day);

            DateTime.TryParseExact(concat, "yyyy-MMMM-d", null, DateTimeStyles.None, out lastDate);

            for (int i = 0; i < dataList.Count; i++)
            {
                lastDate = lastDate.AddDays(1);
                changeBindingDate(i, lastDate);
            }
        }

        // TODO: Put a little marker on today.

        //* Private Methods
        private void changeBindingDate(int i, DateTime date)
        {
            dataList[i].Date = date;

            if (date < DateTime.Today)
                dataList[i].NumOpacity = 0.6;
            else
                dataList[i].NumOpacity = 1;

            if (date == DateSelected)
                selectDay(i);
            else
                dataList[i].CircleOpacity = 0;
        }

        private void changeMonthYearBinding(int n)
        {
            MonthLabel.BindingContext = dataList[n];
            YearLabel.BindingContext = dataList[n];
        }

        private void circleDate(int n)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                if (i != n)
                    dataList[i].CircleOpacity = 0;
            }
            dataList[n].CircleOpacity = 1;
        }

        /// <summary>
        /// Populates the calendar with the week of today and selects today.
        /// </summary>
        private void fillDatesWithToday()
        {
            DateTime today = DateTime.Now;

            // 0 is Sunday ... 6 is Saturday
            int todayOfWeek = (int)today.DayOfWeek;

            changeBindingDate(todayOfWeek, today);

            DateTime temp = today.AddDays(1);

            // Populates days after today
            for (int i = todayOfWeek + 1; i < dataList.Count; i++)
            {
                changeBindingDate(i, temp);
                temp = temp.AddDays(1);
            }

            temp = today;

            // Populates days before today
            for (int i = todayOfWeek - 1; i >= 0; i--)
            {
                temp = temp.AddDays(-1);
                changeBindingDate(i, temp);
            }

            selectDay(todayOfWeek);
        }

        /// <summary>
        /// Updates the ViewModel bindings with new data.
        /// </summary>
        /// <param name="n">
        /// The index of the date on the calendar (0 is the first date, 13 is the last date)
        /// </param>
        private void selectDay(int n)
        {
            changeMonthYearBinding(n);
            circleDate(n);
            setDateSelected(n);

            DateProp.DateString = DateSelected.ToString("dddd, MMMM dd");  //this sets the title to the list view with the dateSelected
        }

        private void setDateSelected(int n)
        {
            int year = dataList[n].Date.Year;
            string month = dataList[n].Month;
            int day = dataList[n].Date.Day;

            string concat = string.Format("{0}-{1}-{2}", year, month, day);

            DateTime.TryParseExact(concat, "yyyy-MMMM-d", null, DateTimeStyles.None, out DateSelected);
        }

        private void setUpDateElements()
        {
            DateTime date = new DateTime(2019, 4, 28);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    dataList.Add(new DayViewModel(date, 0, 1, Color.White));
                    date.AddDays(1);

                    if (i == 0 & j == 0)
                    {
                        YearLabel.BindingContext = dataList[0];
                        MonthLabel.BindingContext = dataList[0];
                        LeftArrowButton.BindingContext = dataList[0];
                        RightArrowButton.BindingContext = dataList[0];
                    }

                    // Label for the date number
                    Label tempLabel = new Label();
                    tempLabel.SetValue(Grid.RowProperty, i + 2);
                    tempLabel.SetValue(Grid.ColumnProperty, j);
                    tempLabel.HorizontalOptions = LayoutOptions.Center;
                    tempLabel.VerticalOptions = LayoutOptions.Center;

                    tempLabel.BindingContext = dataList[i * 7 + j];
                    tempLabel.SetBinding(Label.TextProperty, new Binding(
                        string.Format("{0}.{1}", nameof(DayViewModel.Date), 
                            nameof(DayViewModel.Date.Day))));
                    tempLabel.SetBinding(Label.TextColorProperty, new Binding(
                        nameof(DayViewModel.ColorTheme)));
                    tempLabel.SetBinding(OpacityProperty, new Binding(
                        nameof(DayViewModel.NumOpacity)));

                    MainGrid.Children.Add(tempLabel);

                    // Button behind the Label for the touch event when the number is clicked
                    Button tempButton = new Button();
                    tempButton.SetValue(Grid.RowProperty, i + 2);
                    tempButton.SetValue(Grid.ColumnProperty, j);
                    tempButton.VerticalOptions = LayoutOptions.Center;
                    tempButton.HorizontalOptions = LayoutOptions.Center;
                    tempButton.HeightRequest = 30;
                    tempButton.WidthRequest = 30;
                    tempButton.BackgroundColor = Color.Transparent;
                    tempButton.BorderWidth = 1;
                    tempButton.CornerRadius = 15;
                    tempButton.Clicked += DateButton_Clicked;

                    tempButton.BindingContext = dataList[i * 7 + j];
                    tempButton.SetBinding(Button.BorderColorProperty, new Binding(
                        nameof(DayViewModel.ColorTheme)));
                    tempButton.SetBinding(OpacityProperty, new Binding(
                        nameof(DayViewModel.CircleOpacity)));

                    MainGrid.Children.Add(tempButton);
                }
            }
        }

        //* Event Handlers

        /// <summary>
        /// Handles the event when any of the numbered dates is pressed.
        /// </summary>
        private void DateButton_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int column = (int)button.GetValue(Grid.ColumnProperty);
            int row = (int)button.GetValue(Grid.RowProperty);

            selectDay((row - 2) * 7 + column);

            // Raise the event
            DataSelectedChanged?.Invoke(this, e);
        }

        private void LeftArrowButton_Clicked(object sender, EventArgs e)
        {
            ShiftDatesBackward();
        }

        private void RightArrowButton_Clicked(object sender, EventArgs e)
        {
            ShiftDatesForward();
        }
    }
}