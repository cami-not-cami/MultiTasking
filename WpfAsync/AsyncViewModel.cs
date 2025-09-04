using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAsync
{
    class AsyncViewModel:BaseViewModel
    {
		private string _wert;

		public string Wert
		{
			get { return _wert; }
			set { _wert = value; 
			OnPropertyChanged(nameof(Wert));
            }
		}
		public ICommand CalcCmd { get; set; }
		public AsyncViewModel()
		{
			
			CalcCmd = new RelayCommand(Rechnen);
        }
		private double Calc(double start,double factor,int loop)
		{
			for(int i=0;i<loop;i++)
			{
				start *= factor;
				Thread.Sleep(100);
            }
			return start;
        }
		private async void Rechnen()
		{
			Wert = "Wird berechnet...";
			Task<double> task = Task.Run(() => Calc(123.456, 45, 100));
			await task;
			Wert = task.Result.ToString();
		}
		// private  void Rechnen()
		// {
		//     Wert = "Wird berechnet...";
		//double x = Calc(123.456, 45, 100);

		//     Wert = x.ToString();
		// }
	}
}
