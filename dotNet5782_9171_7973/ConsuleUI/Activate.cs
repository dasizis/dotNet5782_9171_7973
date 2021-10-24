using System;
using System.Collections;
using System.Linq;
using System.Text;
using IDAL.DO;

namespace ConsuleUI
{
    partial class Program
    {
        private const string errorAlert = "Invalid Option";

        public static DalObject.DalObject dalObject = new DalObject.DalObject();

        private static int getInput(string massage = "")
        {
            Console.Write(massage + massage == "" ? "" : "\n");
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int input);

            return input;
        }

        /// <summary>
        /// print an item details acording to its type and id
        /// </summary>
        /// <param name="type">the type of the item</param>
        /// <param name="id">the item id</param>
        private static void printItemById(Type type, int id)
        {
            Console.WriteLine($"{DalObject.DataSource.GetById(type, id)}");
        }

        /// <summary>
        /// activates the main menu
        /// </summary>
        private static void activateMainMenu()
        {
            int input = getInput();
            while (true)
            {

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
                        Console.WriteLine(errorAlert);
                        break;
                }

                printTitle("Main Options");
                printEnum(typeof(MainOption));
                input = getInput();
            }

        }

        /// <summary>
        /// activates the main menu
        /// </summary>
        private static void activateAddMenu()
        {
            printTitle("Add Options");
            printEnum(typeof(AddOption), 1);

            int input = getInput();

            switch ((AddOption)input)
            {
                case AddOption.BaseStation: AddBaseStation(); break;

                case AddOption.Customer: AddCustomer(); break;

                case AddOption.Parcel: AddParcel(); break;

                case AddOption.Drone: AddDrone(); break;

                default: Console.WriteLine(errorAlert); break;
            }
        }

        /// <summary>
        /// activates the display menu
        /// </summary>
        private static void activateDisplayMenu()
        {
            printTitle("Display Options");
            printEnum(typeof(DisplayOption), 1);

            int option = getInput();
            int id;

            switch ((DisplayOption)option)
            {
                case DisplayOption.BaseStation:
                    id = getInput("Number Of Station:");
                    printItemById(typeof(BaseStation), id);
                    break;
                case DisplayOption.Customer:
                    id = getInput("Number Of Customer:");
                    printItemById(typeof(Customer), id);
                    break;
                case DisplayOption.Parcel:
                    id = getInput("Number Of Parcel:");
                    printItemById(typeof(Parcel), id);
                    break;
                case DisplayOption.Drone:
                    id = getInput("Number Of Drone:");
                    printItemById(typeof(Drone), id);
                    break;
                default:
                    Console.WriteLine(errorAlert);
                    break;
            }
        }

        /// <summary>
        /// activates the display list menu
        /// </summary>
        private static void activateDisplayListMenu()
        {
            printTitle("Display List Options");
            printEnum(typeof(DisplayListOption), 1);

            int option = getInput();

            switch ((DisplayListOption)option)
            {
                case DisplayListOption.BaseStation:
                    DisplayList(dalObject.GetBaseStationList());
                    break;
                case DisplayListOption.Customer:
                    DisplayList(dalObject.GetCustomersList());
                    break;
                case DisplayListOption.Parcel:
                    DisplayList(dalObject.GetParcelList());
                    break;
                case DisplayListOption.Drone:
                    DisplayList(dalObject.GetDroneList());
                    break;
                case DisplayListOption.NotAssignedToDroneParcel:
                    DisplayList(dalObject.GetParcelsNotAssignedToDrone());
                    break;
                case DisplayListOption.AvailableBaseStation:
                    DisplayList(dalObject.GetStationsWithEmptySlots());
                    break;
                default:
                    Console.WriteLine(errorAlert);
                    break;
            }
        }

        /// <summary>
        /// activates the update menu
        /// </summary>
        private static void activateUpdateMenu()
        {
            printTitle("Update Options");
            printEnum(typeof(UpdateOption), 1);

            int option = getInput();

            switch ((UpdateOption)option)
            {
                case UpdateOption.AssignParcelToDrone:
                    {
                        int parcelId = getInput($"Parcel ID, Please.");
                        dalObject.AssignParcelToDrone(parcelId);
                        break;
                    }
                case UpdateOption.CollectParcel:
                    {
                        int parcelId = getInput($"Parcel ID, Please.");
                        dalObject.CollectParcel(parcelId);

                        break;
                    }
                case UpdateOption.SupplyParcel:
                    {
                        int parcelId = getInput($"Parcel ID, Please.");
                        dalObject.SupplyParcel(parcelId);

                        break;
                    }
                case UpdateOption.ChargeDroneAtBaseStation:
                    {
                        int droneId = getInput($"Drone ID, Please.");
                        dalObject.ChargeDroneAtBaseStation(droneId);

                        break;
                    }
                case UpdateOption.FinishCharging:
                    {
                        int droneId = getInput($"Drone ID, Please.");
                        dalObject.FinishCharging(droneId);

                        break;
                    }
                default:
                    Console.WriteLine(errorAlert);
                    break;
            }
        }

        /// <summary>
        /// prints a list of items
        /// </summary>
        /// <param name="list">the list to print</param>
        private static void DisplayList(IList list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}