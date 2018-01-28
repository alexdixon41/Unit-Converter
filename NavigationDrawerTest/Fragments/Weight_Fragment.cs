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
using Android.Gms.Ads;

namespace Convert_Stuff
{
    public class Weight_Fragment : Android.Support.V4.App.Fragment
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

            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.weight_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);            

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.weight_units, Resource.Layout.my_spinner);
            outSpinner.Adapter = outAdapter;
            outAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

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

            outText.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
                builder.SetTitle("Output");
                builder.SetMessage(Convert.ToString(outText.Text));
                builder.SetPositiveButton("Done", delegate { });
                builder.Show();
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
                case 0:   //Ounces
                    temp = input / 16;
                    break;
                case 1:   //Pounds
                    temp = input;
                    break;
                case 2:   //Tons
                    temp = input * 2000;
                    break;
                case 3:   //Micrograms
                    temp = input / (4.53592 * (10 ^ 8));
                    break;
                case 4:   //Milligrams
                    temp = input / 453592;
                    break;
                case 5:   //Grams
                    temp = input / 453.592;
                    break;
                case 6:   //Kilograms
                    temp = input * 2.20462;
                    break;
                case 7:   //Megagrams
                    temp = input * 2204.62;
                    break;
            }
            switch (output_id)
            {
                case 0:   //Ounces
                    output = temp * 16;
                    break;
                case 1:   //Pounds
                    output = temp;
                    break;
                case 2:   //Tons
                    output = temp / 2000;
                    break;
                case 3:   //Micrograms
                    output = temp * 453592000;
                    break;
                case 4:   //Milligrams
                    output = temp * 453592;
                    break;
                case 5:   //Grams
                    output = temp * 453.592;
                    break;
                case 6:   //Kilograms
                    output = temp / 2.20462;
                    break;
                case 7:   //Megagrams
                    output = temp / 2204.62;
                    break;
            }
            return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}