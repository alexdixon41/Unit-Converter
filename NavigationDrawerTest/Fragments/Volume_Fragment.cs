using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Convert_Stuff
{
    public class Volume_Fragment : Android.Support.V4.App.Fragment
    {
        // Conversion constants - factors that convert each unit into quarts
        const double LITERS = 1.05669;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Unsigned_conversion_layout, container, false);

            EditText inText = view.FindViewById<EditText>(Resource.Id.input);
            TextView outText = view.FindViewById<TextView>(Resource.Id.outputText);
            Spinner inSpinner = view.FindViewById<Spinner>(Resource.Id.inSpinner);
            Spinner outSpinner = view.FindViewById<Spinner>(Resource.Id.outSpinner);

            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.volume_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);            

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.volume_units, Resource.Layout.my_spinner);
            outSpinner.Adapter = outAdapter;
            outAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            outText.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
                builder.SetTitle("Output");
                builder.SetMessage(Convert.ToString(outText.Text));
                builder.SetPositiveButton("Done", delegate { });
                builder.Show();
            };

            outSpinner.ItemSelected += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                }
                else
                {
                    outText.Text = "";
                }
            };

            var convert = view.FindViewById(Resource.Id.convert_btn);
            convert.Click += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                }
                else
                {
                    Toast.MakeText(this.Context, "Enter decimal value", ToastLength.Long).Show();
                }
            };

            return view;
        }

        private string Convert_units(double input, long input_id, long output_id)
        {
            double output = 0;
            double temp = 0;
            switch (input_id)
            {
                case 0:   //Teaspoons
                    temp = input / 191.997;
                    break;
                case 1:   //Tablespoons
                    temp = input / 63.999;
                    break;
                case 2:   //Ounces
                    temp = input / 31.9995;
                    break;
                case 3:   //Cups
                    temp = input / 3.94314;
                    break;
                case 4:   //Pints
                    temp = input / 2;
                    break;
                case 5:   //Quarts
                    temp = input;
                    break;
                case 6:   //Gallons
                    temp = input * 4;
                    break;
                case 7:   //Milliliters / Cubic Centimeters
                    temp = input / 946.353;
                    break;
                case 8:   //Liters
                    temp = input * LITERS;
                    break;
                case 9:   //Cubic Inches
                    temp = input / 57.7502;
                    break;
                case 10:  //Cubic Feet
                    temp = input * 29.9221;
                    break;
                case 11:  //Cubic Yards
                    temp = input * 807.896;
                    break;
                case 12:  //Cubic Millimeters
                    temp = input / 946352.946;
                    break;
                case 13:  //Cubic Meters
                    temp = input * 1056.69;
                    break;
                case 14:  //Bushels
                    temp = input * 37.2367;
                    break;
            }
            switch (output_id)
            {
                case 0:   //Teaspoons
                    output = temp * 191.997;
                    break;
                case 1:   //Tablespoons
                    output = temp * 63.999;
                    break;
                case 2:   //Ounces
                    output = temp * 31.9995;
                    break;
                case 3:   //Cups
                    output = temp * 3.94314;
                    break;
                case 4:   //Pints
                    output = temp * 2;
                    break;
                case 5:   //Quarts
                    output = temp;
                    break;
                case 6:   //Gallons
                    output = temp / 4;
                    break;
                case 7:   //Milliliters / Cubic Centimeters
                    output = temp * 946.353;
                    break;
                case 8:   //Liters
                    output = temp / LITERS;
                    break;
                case 9:   //Cubic Inches
                    output = temp * 57.7502;
                    break;
                case 10:  //Cubic Feet
                    output = temp / 29.9221;
                    break;
                case 11:  //Cubic Yards
                    output = temp / 807.896;
                    break;
                case 12:  //Cubic Millimeters
                    output = temp * 946352.946;
                    break;
                case 13:  //Cubic Meters
                    output = temp / 1056.69;
                    break;
                case 14:  //Bushels
                    output = temp / 37.2367;
                    break;
            }
            return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}