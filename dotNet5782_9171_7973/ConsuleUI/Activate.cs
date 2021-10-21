using System;
using IDAL.DO;

namespace ConsuleUI
{
    partial class Program
    {        
        private static int getInput()
        {
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int input);

            return input;
        }

        private static void printItemById(Type type, int id)
        {
            Console.WriteLine($"\t{DalObject.DataSource.GetById(type, id)}");
        }

        private static void activateMainMenu()
        {
            int input = getInput();

            switch ((MainOption)input)
            {
                case MainOption.Add:
                    activateAddMenu();
                    break;
                case MainOption.Update:
                    activateUpdateMenu();
                    break;
                case MainOption.Display:
                    activateDisplayMenu();
                    break;
                case MainOption.DisplayList:
                    activateDisplayListMenu();
                    break;
                case MainOption.Exit:
                    return;
                default:
                    break;
            }
        }

        private static void activateAddMenu()
        {
            printTitle("Add Options");
            printEnum(typeof(AddOption), 1);

            int input = getInput();

            switch ((AddOption)input)
            {
                case AddOption.BaseStation:
                    break;
                case AddOption.Customer:
                    break;
                case AddOption.Parcel:
                    break;
                case AddOption.Drone:
                    break;
                default:

                    break;
            }

        }

        private static void activateDisplayMenu()
        {
            printTitle("Display Options");
            printEnum(typeof(DisplayOption), 1);

            int id = getInput();

            switch ((DisplayOption)id)
            {
                case DisplayOption.BaseStation:
                    printItemById(typeof(BaseStation), id);
                    break;
                case DisplayOption.Customer:
                    printItemById(typeof(Customer), id);
                    break;
                case DisplayOption.Parcel:
                    printItemById(typeof(Parcel), id);
                    break;
                case DisplayOption.Drone:
                    printItemById(typeof(Drone), id);
                    break;
                default:
                    break;
            }                
        }

        private static void activateDisplayListMenu()
        {
            printTitle("Display List Options");
            printEnum(typeof(DisplayListOption), 1);

            int id = getInput();

            switch ((DisplayListOption)id)
            {
                case DisplayListOption.BaseStation:
                    break;
                case DisplayListOption.Customer:
                    break;
                case DisplayListOption.Parcel:
                    break;
                case DisplayListOption.Drone:
                    break;
                default:
                    break;
            }
        }

        private static void activateUpdateMenu()
        {
            printTitle("Update Options");
            printEnum(typeof(UpdataOption), 1);

            int id = getInput();

            switch ((DisplayListOption)id)
            {
                case DisplayListOption.BaseStation:
                    break;
                case DisplayListOption.Customer:
                    break;
                case DisplayListOption.Parcel:
                    break;
                case DisplayListOption.Drone:
                    break;
                default:
                    break;
            }
        }

    }
}