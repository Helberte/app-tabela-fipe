using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;

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
            List<Menu> menus = new()
            {
                new Menu() { Titulo = "Consultar veículo", Subtitulo = "Aqui você acha o preço médio do seu veiculo", Icone = Resource.Drawable.icons8_lupa_48 },
                new Menu() { Titulo = "Meus veículos", Subtitulo = "Aqui você acha os seus veículos salvos.", Icone = Resource.Drawable.icons8_vehicle_48 }
            };

            return menus;
        }

        public string GetSaudacao()
        {
            int horaAtual = DateTime.Now.Hour;

            return horaAtual switch
            {
                < 6  => "Boa madrugada!",
                < 12 => "Bom dia!",
                < 18 => "Boa tarde!",
                _ => "Boa noite!"
            };
        }

        #endregion

        #region Adapters

        public class MenuHolder : RecyclerView.ViewHolder
        {
            public TextView Texto { get; private set; }

            public ImageView Icone { get; private set; }

            public TextView Subtitulo { get; private set; } 

            public MenuHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Texto     = itemView.FindViewById<TextView> (Resource.Id.activity_main_Row_lblTexto);
                Icone     = itemView.FindViewById<ImageView>(Resource.Id.activity_main_Row_imgIconeConsulta);
                Subtitulo = itemView.FindViewById<TextView> (Resource.Id.activity_main_Row_lblSubTitulo); 

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

                holderMenu.Texto.Text = item.Titulo;
                holderMenu.Subtitulo.Text = item.Subtitulo;
                holderMenu.Icone.SetImageResource(item.Icone);
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
            public string Titulo { get; set; }
            public int Icone { get; set; }
            public string Subtitulo { get; set; } 
        }

        #endregion

    }
}