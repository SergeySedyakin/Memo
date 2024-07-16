using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;
using Android.Media;

namespace Memo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnRules, btnNewGame;
        public static MediaPlayer mp = new MediaPlayer(); //плеер для воспроизведения звука
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource   #2c3e50
            SetContentView(Resource.Layout.activity_main);

            btnNewGame = FindViewById<Button>(Resource.Id.btnNewGameSixteen);
            btnRules = FindViewById<Button>(Resource.Id.btnRules);

            btnRules.Click += BtnRules_Click;
            btnNewGame.Click += BtnNewGame_Click;
        }

        private void BtnNewGame_Click(object sender, System.EventArgs e)
        {
            mp = MediaPlayer.Create(this, Resource.Raw.clickButtonApp);
            mp.Start();
            Intent actGame = new Intent(this, typeof(ActivityGame));
            StartActivity(actGame);
        }

        private void BtnRules_Click(object sender, System.EventArgs e)
        {
            mp = MediaPlayer.Create(this, Resource.Raw.clickButtonApp);
            mp.Start();
            string text = "В игре МЕМО главная задача - найти пары всех картинок.\nПри нажатии на одну закрытую картинку она становится открытой.\nДалее нужно найти такую же картинку, если вы не угадали - картинки закрываются.";
            new Android.App.AlertDialog.Builder(this)
                .SetTitle("Правила игры")
                .SetMessage(text)
                .SetPositiveButton("OK", delegate { })
                .SetCancelable(false)
                .Show();
                
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}