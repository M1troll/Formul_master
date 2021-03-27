using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Model;

namespace BL
{
    public class Logic:INotifyPropertyChanged
    {
        /*
         * \u03A3 - сигма
         * \u207F - верхний индекс n
         * \u1D62 - нижний индекс i
         * \u208C - нижний индекс "="
         * \u2081 - нижний индекс единица
         * \u00B2 - знак квадрата
         * \u00B3 - знак куба
         * \u2265 - больше или равно
         * \u2264 - меньше или равно
         */
        public Formula SelectedFormula { get; set; }
        public decimal Input_N { get; set; }
        public string Result { get;set; }
        public ObservableCollection<Formula> formuls { get; set; } = new ObservableCollection<Formula>()
        {
            new Formula("\u03A3\u207F\u1D62\u208C\u2081 i\u00B2",
                                       "n(n+1)(2n+1)/6",
                                       "n\u22650",
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = 1; i <= n; i++)
                                                 {
                                                     sum += i * i;
                                                 }
                                                 return sum;
                                             },
                                       (n) =>
                                             {
                                                 return (n*(n + 1)*(2*n + 1))/6;
                                             },
                                       (n) =>
                                             {
                                               if (n >=0) { return true; }
                                               else { return false; }
                                             }
                                        ),
            new Formula("\u03A3\u207F\u1D62\u208C\u2081 i\u00B3",
                                       "n\u00B2*(n+1)\u00B2/4",
                                       "n\u22650",
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = 1; i <= n; i++)
                                                 {
                                                     sum += i * i * i;
                                                 }
                                                 return sum;
                                             },
                                       (n) =>
                                             {
                                                 return (decimal)(((n*n)*(n+1)*(n+1))/4);
                                             },
                                       (n) =>
                                             {
                                               if (n >=0) { return true; }
                                               else { return false; }
                                             }
                                        ),
             new Formula("\u03A3\u207F\u1D62\u208C\u2081 i(i+1)(i+2)",
                                       "n(n+1)(n+2)(n+3)/4",
                                       "n\u22650",
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = 1; i <= n; i++)
                                                 {
                                                     sum += i*(i+1)*(i+2);
                                                 }
                                                 return sum;
                                             },
                                       (n) =>
                                             {
                                                 return (decimal)((n*(n+1)*(n+2)*(n+3))/4);
                                             },
                                       (n) =>
                                             {
                                               if (n >=0) { return true; }
                                               else { return false; }
                                             }
                                        ),
             new Formula("\u03A3\u207F\u1D62\u208C\u2081 1/(i(i+1))",
                                       "n/(n+1)",
                                       "n\u22650",
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = 1; i <= n; i++)
                                                 {
                                                    sum += 1/((decimal)(i*(i+1)));
                                                 }
                                                 return sum;
                                             },
                                       (n) =>
                                             {
                                                 return (decimal)(n/(n+1));
                                             },
                                       (n) =>
                                             {
                                               if (n>=0) { return true; }
                                               else { return false; }
                                             }
                                        ),
             new Formula("\u03A3\u207F\u1D62\u208C\u2080 (n-i)",
                                       "\u03A3\u207F\u1D62\u208C\u2080 i",
                                       "n\u22650",
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = 0; i <= n; i++)
                                                 {
                                                     sum += n-i; //при делении привести к (decimal)
                                                 }
                                                 return sum;
                                             },
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = 0; i <= n; i++)
                                                {
                                                    sum +=i; //при делении привести к (decimal)
                                                }
                                                return sum;
                                                //return ***; //при делении привести к (decimal)
                                             },
                                       (n) =>
                                             {
                                               if (n>=0) { return true; }
                                               else { return false; }
                                             }
                                        ),
            /*
               new Formula("\u03A3\u207F\u1D62\u208C *** ***",
                                       "***",
                                       "***",
                                       (n) =>
                                             {
                                                decimal sum = 0;
                                                for(int i = *; i <= n; i++)
                                                 {
                                                     sum += ***; //при делении привести к (decimal)
                                                 }
                                                 return sum;
                                             },
                                       (n) =>
                                             {
                                                 return ***; //при делении привести к (decimal)
                                             },
                                       (n) =>
                                             {
                                               if (n ***) { return true; }
                                               else { return false; }
                                             }
                                        ),
            */
        };
        private void Answer()
        {
            if (SelectedFormula != null)
            {
                if (SelectedFormula.Check(Input_N))
                {
                    Result = $"\u03A3 : {SelectedFormula.F_Sigma(Input_N)}\nФормула : {SelectedFormula.F_Expression(Input_N)}";
                }
                else { Result = "Некорректное число n\r\n"; }
                OnPropertyChanged("Result");
            }
            else
            {
                Result = "Выберите формулу\r\n";
                OnPropertyChanged("Result");
            }
        }

        private ICommand _answer;
        public ICommand CommandAnswer
        {
            get { return _answer ?? (_answer= new RelayCommand(Answer)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this,new PropertyChangedEventArgs(prop));
        }
    }
}
