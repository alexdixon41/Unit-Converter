using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Convert_Stuff
{
    public class Pressure_Fragment : Android.Support.V4.App.Fragment
    {
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

            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.pressure_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.pressure_units, Resource.Layout.my_spinner);
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
                case 0:   //Pascal
                    temp = input / 101325;
                    break;
                case 1:   //Bar
                    temp = input / 1.01325;
                    break;
                case 2:   //Atmosphere
                    temp = input;
                    break;
                case 3:   //Torr
                    temp = input / 760;
                    break;
                case 4:   //Pounds per square inch
                    temp = input / 14.69595;
                    break;
            }
            switch (output_id)
            {
                case 0:   //Pascal
                    output = temp * 101325;
                    break;
                case 1:   //Bar
                    output = temp * 1.01325;
                    break;
                case 2:   //Atmosphere
                    output = temp;
                    break;
                case 3:   //Torr
                    output = temp * 760;
                    break;
                case 4:   //Pounds per square inch
                    output = temp * 14.69595;
                    break;
            }
            return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}