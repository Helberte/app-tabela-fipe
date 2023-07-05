using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace Tabela_FIPE
{
    [Activity(Label = "Tebala-FIPE", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Variáveis

        private static RecyclerView MainActivity_recyclerMenu = null;
        private static TextView     MainActivity_lblSaudacao  = null;

        #endregion

        #region Overrides

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Exibe a tela principal
            ExibeLayoutMenu();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion

        #region Exibição e instancias das Telas (Layouts)

        private void ExibeLayoutMenu()
        {
            SetContentView(Resource.Layout.activity_main);

            MainActivity_recyclerMenu = FindViewById<RecyclerView>(Resource.Id.MainActivity_recyclerMenu);
            MainActivity_lblSaudacao  = FindViewById<TextView>    (Resource.Id.MainActivity_lblSaudacao);

            MainActivity_lblSaudacao.Text = GetSaudacao();

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            MenuAdapter adapter = new MenuAdapter(GetMenus());
            MainActivity_recyclerMenu.HasFixedSize = true;
            MainActivity_recyclerMenu.SetLayoutManager(gridLayoutManager);
            MainActivity_recyclerMenu.SetAdapter(adapter);
        }

        #endregion

        #region Metodos

        public List<Menu> GetMenus()
        {
            List<Menu> menus = new List<Menu>();

            menus.Add(new Menu() { Nome = "Item 1" });
            menus.Add(new Menu() { Nome = "Item 2" });
            menus.Add(new Menu() { Nome = "Item 3" });

            return menus;
        }

        public string GetSaudacao()
        {
            string horaAtual = DateTime.Now.ToString("HH");
            string saudacao = "Bom dia!";

            if (Convert.ToInt32(horaAtual) >= 12 && Convert.ToInt32(horaAtual) < 18)            
                saudacao = "Boa tarde!";            
            else if (Convert.ToInt32(horaAtual) >= 18 && Convert.ToInt32(horaAtual) <= 23)            
                saudacao = "Boa noite!";            
            else if (Convert.ToInt32(horaAtual) >= 0 && Convert.ToInt32(horaAtual) <= 6)            
                saudacao = "Boa madrugada!";
            
            return saudacao;
        }

        #endregion

        #region Adapters

        public class MenuHolder : RecyclerView.ViewHolder
        {
            public TextView Texto { get; private set; }
             
            public MenuHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Texto = itemView.FindViewById<TextView>(Resource.Id.activity_main_Row_lblTexto);
                
                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        public class MenuAdapter : RecyclerView.Adapter
        {
            public List<Menu> items;
            public event EventHandler<int> ItemClick;

            public MenuAdapter(List<Menu> items)
            {
                this.items = items;
            }

            public override int ItemCount => items.Count;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Menu item = items[position];
                MenuHolder holderMenu = holder as MenuHolder;

                holderMenu.Texto.Text = item.Nome;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.activity_main_Row, parent, false);

                MenuHolder vh = new MenuHolder(itemView, OnClick);
                return vh;
            }

            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }
        }

        #endregion

        #region Comum

        public class Menu
        {
            public string Nome { get; set; }
        }

        #endregion

    }
}