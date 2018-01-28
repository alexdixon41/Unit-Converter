using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Gms.Ads;
using Android.Views.InputMethods;

namespace Convert_Stuff
{
    [Activity(Label = "Convert Stuff", MainLauncher = true, Icon = "@drawable/main_launcher", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : AppCompatActivity
	{
		private DrawerLayout drawerLayout;
        private SupportFragment currentFragment = new Weight_Fragment();
        private Weight_Fragment mWeightFragment;
        private Distance_Fragment mDistanceFragment;
        private Volume_Fragment mVolumeFragment;
        private Help_Fragment mHelpFragment;
        private Temperature_Fragment mTempFragment;
        private Time_Fragment mTimeFragment;
        private Data_Fragment mDataFragment;
        private Pressure_Fragment mPressureFragment;
        private Energy_Fragment mEnergyFragment;
        private Force_Fragment mForceFragment;

        protected override void OnCreate(Bundle bundle)
		{			
			base.OnCreate(bundle);

			// Create UI
			SetContentView(Resource.Layout.Main);
			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

			// Init toolbar
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);
            toolbar.Title = "Convert Stuff";

            // Ads
            AdView adView = FindViewById<AdView>(Resource.Id.adView);
            AdRequest.Builder adRequest = new AdRequest.Builder();
            adRequest.AddTestDevice("BAAAA80F92B4A5B643F09491CD7BC741").AddTestDevice("62AC86B88A0AFB414A990F27D6268BB6");
            adView.LoadAd(adRequest.Build());

            // Hide Soft Keyboard when NavDrawer is open
            drawerLayout.DrawerOpened += delegate
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, 0);
            };

            //Init Fragments
            mWeightFragment = new Weight_Fragment();
            mDistanceFragment = new Distance_Fragment();
            mVolumeFragment = new Volume_Fragment();
            mHelpFragment = new Help_Fragment();
            mTempFragment = new Temperature_Fragment();
            mTimeFragment = new Time_Fragment();
            mDataFragment = new Data_Fragment();
            mPressureFragment = new Pressure_Fragment();
            mEnergyFragment = new Energy_Fragment();
            mForceFragment = new Force_Fragment();

            //Add Fragments to Manager
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, mHelpFragment, "Help");
            trans.Hide(mHelpFragment);
            trans.Add(Resource.Id.fragmentContainer, mDistanceFragment, "Distance");
            trans.Hide(mDistanceFragment);
            trans.Add(Resource.Id.fragmentContainer, mVolumeFragment, "Volume");
            trans.Hide(mVolumeFragment);            
            trans.Add(Resource.Id.fragmentContainer, mTempFragment, "Temperature");
            trans.Hide(mTempFragment);
            trans.Add(Resource.Id.fragmentContainer, mPressureFragment, "Pressure");
            trans.Hide(mPressureFragment);
            trans.Add(Resource.Id.fragmentContainer, mEnergyFragment, "Energy");
            trans.Hide(mEnergyFragment);
            trans.Add(Resource.Id.fragmentContainer, mForceFragment, "Force");
            trans.Hide(mForceFragment);
            trans.Add(Resource.Id.fragmentContainer, mTimeFragment, "Time");
            trans.Hide(mTimeFragment);
            trans.Add(Resource.Id.fragmentContainer, mDataFragment, "Data Storage");
            trans.Hide(mDataFragment);
            trans.Add(Resource.Id.fragmentContainer, mWeightFragment, "Weight");
            trans.Commit();
            currentFragment = mWeightFragment;

            // Attach item selected handler to navigation view
			var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

			// Create ActionBarDrawerToggle button and add it to the toolbar
			var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
			drawerLayout.AddDrawerListener(drawerToggle);
			drawerToggle.SyncState();
           
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.toolbarmenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) 
            {
                case Resource.Id.help:
                    drawerLayout.CloseDrawers();
                    ShowFragment(mHelpFragment);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void ShowFragment(SupportFragment fragment)
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Hide(currentFragment);
            trans.Show(fragment);            
            trans.Commit();
            currentFragment = fragment;
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_weight):
                    toolbar.Title = "Weight";
                    ShowFragment(mWeightFragment);
                    break;
                case (Resource.Id.nav_distance):
                    toolbar.Title = "Distance";
                    ShowFragment(mDistanceFragment);
                    break;
                case (Resource.Id.nav_volume):
                    toolbar.Title = "Volume";
                    ShowFragment(mVolumeFragment);
                    break;
                case (Resource.Id.nav_temperature):
                    toolbar.Title = "Temperature";
                    ShowFragment(mTempFragment);
                    break;
                case (Resource.Id.nav_pressure):
                    toolbar.Title = "Pressure";
                    ShowFragment(mPressureFragment);
                    break;
                case (Resource.Id.nav_energy):
                    toolbar.Title = "Energy";
                    ShowFragment(mEnergyFragment);
                    break;
                case (Resource.Id.nav_force):
                    toolbar.Title = "Force";
                    ShowFragment(mForceFragment);
                    break;
                case (Resource.Id.nav_time):
                    toolbar.Title = "Time";
                    ShowFragment(mTimeFragment);
                    break;
                case (Resource.Id.nav_data):
                    toolbar.Title = "Data Storage";
                    ShowFragment(mDataFragment);
                    break;
                case (Resource.Id.help):
                    toolbar.Title = "Help";
                    ShowFragment(mHelpFragment);
                    break;
            }

            // Close drawer
            drawerLayout.CloseDrawers();
		}
	}
}