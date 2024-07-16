using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Memo
{
    [Activity(Label = "ActivityGame")]
    public class ActivityGame : Activity
    {
        Button btnExit;
        ImageButton imgbtn11, imgbtn12, imgbtn13, imgbtn14, imgbtn21, imgbtn22, imgbtn23, imgbtn24, imgbtn31, imgbtn32, imgbtn33, imgbtn34, imgbtn41, imgbtn42, imgbtn43, imgbtn44, imgbtn51, imgbtn52, imgbtn53, imgbtn54;
        Hashtable hashImage = new Hashtable(); // словарь, хранящий "кнопку - индекс картинки" 
        Random rnd = new Random();
        public static MediaPlayer mp = new MediaPlayer(); //плеер для воспроизведения звука

        List<int> OpenImageIndices = new List<int>(); // список, хранящий индексы открытых картинок
        List<ImageButton> tmpImgBnt = new List<ImageButton>(); //временный список для двух недавно нажатых кнопок
        int CountSteps = 0, PairsFind = 0; //количество ходов и количество найденных пар
        int imageCardBack = Resource.Drawable.Back; // изображение рубашки карты

        /// <summary>
        /// Массив с изображением карточек
        /// </summary>
        int[] images = {Resource.Drawable.Smile, Resource.Drawable.Sun, Resource.Drawable.RedStar, Resource.Drawable.Lightning, Resource.Drawable.House, Resource.Drawable.Heart, Resource.Drawable.GreenStar, Resource.Drawable.Flover, Resource.Drawable.Cloud, Resource.Drawable.YellowStar};
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutGame);

            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            imgbtn11 = FindViewById<ImageButton>(Resource.Id.imgbtn11);
            imgbtn12 = FindViewById<ImageButton>(Resource.Id.imgbtn12);
            imgbtn13 = FindViewById<ImageButton>(Resource.Id.imgbtn13);
            imgbtn14 = FindViewById<ImageButton>(Resource.Id.imgbtn14);
            imgbtn21 = FindViewById<ImageButton>(Resource.Id.imgbtn21);
            imgbtn22 = FindViewById<ImageButton>(Resource.Id.imgbtn22);
            imgbtn23 = FindViewById<ImageButton>(Resource.Id.imgbtn23);
            imgbtn24 = FindViewById<ImageButton>(Resource.Id.imgbtn24);
            imgbtn31 = FindViewById<ImageButton>(Resource.Id.imgbtn31);
            imgbtn32 = FindViewById<ImageButton>(Resource.Id.imgbtn32);
            imgbtn33 = FindViewById<ImageButton>(Resource.Id.imgbtn33);
            imgbtn34 = FindViewById<ImageButton>(Resource.Id.imgbtn34);
            imgbtn41 = FindViewById<ImageButton>(Resource.Id.imgbtn41);
            imgbtn42 = FindViewById<ImageButton>(Resource.Id.imgbtn42);
            imgbtn43 = FindViewById<ImageButton>(Resource.Id.imgbtn43);
            imgbtn44 = FindViewById<ImageButton>(Resource.Id.imgbtn44);
            imgbtn51 = FindViewById<ImageButton>(Resource.Id.imgbtn51);
            imgbtn52 = FindViewById<ImageButton>(Resource.Id.imgbtn52);
            imgbtn53 = FindViewById<ImageButton>(Resource.Id.imgbtn53);
            imgbtn54 = FindViewById<ImageButton>(Resource.Id.imgbtn54);

            //список, элементами которого являются ImageButton-ы
            List<ImageButton> imgBtnArray = new List<ImageButton> { imgbtn11, imgbtn12, imgbtn13, imgbtn14, imgbtn21, imgbtn22, imgbtn23, imgbtn24, imgbtn31, imgbtn32, imgbtn33, imgbtn34, imgbtn41, imgbtn42, imgbtn43, imgbtn44, imgbtn51, imgbtn52, imgbtn53, imgbtn54};

            //присваивание карточкам их картинки в случайном порядке
            for (int i = 0; i < images.Length * 2; i++)
            {                
                int indexBtn = rnd.Next(0, imgBtnArray.Count);
                ImageButton rndBtn = imgBtnArray[indexBtn];
                rndBtn.SetImageResource(imageCardBack);
                if (i >= images.Length)
                {
                    hashImage[rndBtn] = i - images.Length;
                }else hashImage[rndBtn] = i;
                imgBtnArray.RemoveAt(indexBtn);
            }

            btnExit.Click += BtnExit_Click;

            imgbtn11.Click += Imgbtn11_Click;
            imgbtn12.Click += Imgbtn11_Click;
            imgbtn13.Click += Imgbtn11_Click;
            imgbtn14.Click += Imgbtn11_Click;
            imgbtn21.Click += Imgbtn11_Click;
            imgbtn22.Click += Imgbtn11_Click;
            imgbtn23.Click += Imgbtn11_Click;
            imgbtn24.Click += Imgbtn11_Click;
            imgbtn31.Click += Imgbtn11_Click;
            imgbtn32.Click += Imgbtn11_Click;
            imgbtn33.Click += Imgbtn11_Click;
            imgbtn34.Click += Imgbtn11_Click;
            imgbtn41.Click += Imgbtn11_Click;
            imgbtn42.Click += Imgbtn11_Click;
            imgbtn43.Click += Imgbtn11_Click;
            imgbtn44.Click += Imgbtn11_Click;
            imgbtn51.Click += Imgbtn11_Click;
            imgbtn52.Click += Imgbtn11_Click;
            imgbtn53.Click += Imgbtn11_Click;
            imgbtn54.Click += Imgbtn11_Click;
        }
        
        private void Imgbtn11_Click(object sender, EventArgs e)
        {            
            if (OpenImageIndices.Count < 2)
            {
                ImageButton Ibn = (ImageButton)sender;
                int i = (int)hashImage[Ibn];
                Ibn.SetImageResource(images[i]);
                Ibn.Enabled = false;
                OpenImageIndices.Add(i);
                tmpImgBnt.Add(Ibn);
                mp = MediaPlayer.Create(this, Resource.Raw.clickButtonApp);
                mp.Start();
            }
            else 
            {
                if (OpenImageIndices[0] != OpenImageIndices[1])
                {
                    mp = MediaPlayer.Create(this, Resource.Raw.falseAnsw);
                    mp.Start();
                    foreach (ImageButton item in tmpImgBnt)
                    {
                        item.Enabled = true;
                        item.SetImageResource(imageCardBack);
                    }                    
                }
                else if (OpenImageIndices[0] == OpenImageIndices[1])
                {
                    mp = MediaPlayer.Create(this, Resource.Raw.trueAnsw);
                    mp.Start();
                    hashImage.Remove(OpenImageIndices[0].ToString());
                    hashImage.Remove(OpenImageIndices[1].ToString());
                    PairsFind++;
                    if (PairsFind == images.Length-1)
                    {
                        PairsFind++;
                        CountSteps++;
                        GameOver();
                    }
                }
                OpenImageIndices.Clear();
                tmpImgBnt.Clear();
                CountSteps++;
            }    
        }
        void GameOver()
        {
            new Android.App.AlertDialog.Builder(this)
                    .SetTitle("Игра окончена")
                    .SetMessage($"Количество шагов: {CountSteps}\nПар найдено: {PairsFind}")
                    .SetNeutralButton("OK", delegate { Finish(); })
                    .SetCancelable(false)
                    .Show();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            mp = MediaPlayer.Create(this, Resource.Raw.clickButtonApp);
            mp.Start();
            GameOver();
        }
    }
}