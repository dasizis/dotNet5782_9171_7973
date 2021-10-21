using System;
using System.Collections;
using System.Linq;
using System.Text;
using IDAL.DO;

namespace ConsuleUI
{
    partial class Program
    {
        public static DalObject.DalObject dalObject = new DalObject.DalObject();
        private static int getInput(string massage = "")
        {
            Console.Write(massage+ ">");
            int.TryParse(Console.ReadLine(), out int input);

            return input;
        }

        private static void printItemById(Type type, int id)
        {
            Console.WriteLine($"{DalObject.DataSource.GetById(type, id)}");
        }

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
                        break;
                }
                
                printTitle("Main Options");
                printEnum(typeof(MainOption));
                input = getInput();
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
                    {
                      
                        printHeader("Enter Details, Please.");
                        Console.WriteLine("-name, -longitude, -latitude, -number of charge slots");
                        string name = Console.ReadLine();
                        double longitude, latitude;
                        double.TryParse(Console.ReadLine(), out longitude);
                        double.TryParse(Console.ReadLine(), out latitude);
                        int chargeSlots = getInput();

                        dalObject.AddBaseStation(name, longitude, latitude, chargeSlots);
                        DisplayList(dalObject.GetBaseStationList());
                        break;
                    }
                case AddOption.Customer:
                    {
                        printHeader("Enter Details, Please.");
                        Console.WriteLine("-name, -longitude, -latitude, -phone number");
                        string name = Console.ReadLine();
                        double longitude, latitude;
                        double.TryParse(Console.ReadLine(), out longitude);
                        double.TryParse(Console.ReadLine(), out latitude);
                        string phone = Console.ReadLine();

                        dalObject.AddCustomer(name, longitude, latitude, phone);
                        break;
                    }
                case AddOption.Parcel:
                    {
                        printHeader("Enter Details, Please.");
                        Console.WriteLine("-sender ID, -target ID, -whight (0-2), priority (0-2)");
                        int senderId = getInput();
                        int targetId = getInput();
                        WeightCategory weight = (WeightCategory)getInput();
                        Priority priority = (Priority)getInput();

                        dalObject.AddParcel(senderId, targetId, weight, priority);
                        break;
                    }
                case AddOption.Drone:
                    {
                        printHeader("Enter Details, Please.");
                        string model = Console.ReadLine();
                        WeightCategory weight = (WeightCategory)getInput();
                        DroneStatus status = (DroneStatus)getInput();

                        dalObject.AddDrone(model, weight, status);
                        break;
                    }
                default:

                    break;
            }

        }

        private static void activateDisplayMenu()
        {
            printTitle("Display Options");
            printEnum(typeof(DisplayOption), 1);

            int id = getInput();
            int index;

            switch ((DisplayOption)id)
            {
                case DisplayOption.BaseStation:
                    index = getInput("Number Of Station:");
                    printItemById(typeof(BaseStation), index);
                    break;
                case DisplayOption.Customer:
                    index = getInput("Number Of Customer:");
                    printItemById(typeof(Customer), index);
                    break;
                case DisplayOption.Parcel:
                    index = getInput("Number Of Parcel:");
                    printItemById(typeof(Parcel), index);
                    break;
                case DisplayOption.Drone:
                    index = getInput("Number Of Drone:");
                    printItemById(typeof(Drone), index);
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
                    break;
            }
        }

        private static void activateUpdateMenu()
        {
           
            printTitle("Update Options");
            printEnum(typeof(UpdateOption), 1);

            int id = getInput();

            switch ((UpdateOption)id)
            {
                case UpdateOption.AssignParcelToDrone:
                    {
                        int parcelId = getInput(
                            $"Number Of Parcel, Please." +
                            $" (0 - {DalObject.DataSource.GetNumberOfItems(typeof(Parcel))})\n"
                        );
                       
                        dalObject.AssignParcelToDrone(parcelId);
                        
                        break;
                    }
                case UpdateOption.CollectParcel:
                    {
                        int parcelId = getInput(
                            $"Number Of Parcel, Please." +
                            $" (0 - {DalObject.DataSource.GetNumberOfItems(typeof(Parcel))})\n"
                        );
                       
                        dalObject.CollectParcel(parcelId);

                        break;
                    }
                case UpdateOption.ChargeDroneAtBaseStation:
                    {
                        int droneId = getInput(
                            $"Number Of Drone, Please." +
                            $" (0 - {DalObject.DataSource.GetNumberOfItems(typeof(Drone))}) \n"
                            );

                        dalObject.ChargeDroneAtBaseStation(droneId);
                        
                        break;
                    }
                case UpdateOption.FinishCharging:
                    {
                        int droneId = getInput(
                            $"Number Of Drone, Please." +
                            $" (0 - {DalObject.DataSource.GetNumberOfItems(typeof(Drone))}) \n"
                            );

                        dalObject.FinishCharging(droneId);

                        break;
                    }
                default:
                    break;
            }
        }

        private static void DisplayList(IList list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

    }
}

