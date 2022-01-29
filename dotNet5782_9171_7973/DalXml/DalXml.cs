using Singleton;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{

    /// <summary>
    /// Implements the <see cref="DalApi.IDal"/> interface using objects to store the data
    /// </summary>
    public sealed partial class DalXml : Singleton<DalXml>, DalApi.IDal
    {

        private readonly string XmlFilesLocation = $@"{Directory.GetCurrentDirectory()}\..\..\..\..\XmlData";

        private string GetXmlFilePath(Type type) => $@"{XmlFilesLocation}\{type.Name}.xml";

        private string ConfigFilePath => $@"{XmlFilesLocation}\config.xml";

        private DalXml() { }

        static DalXml() { }

        #region Create

        public void Add<T>(T item) where T : IIdentifiable, IDeletable
        {
            Type type = typeof(T);
            if (DoesExist<T>(obj => obj.Id == item.Id))
            {
                throw new IdAlreadyExistsException(type, item.Id);
            }

            item.IsDeleted = false;

            XDocument document = XDocument.Load(GetXmlFilePath(typeof(T)));
            document.Root.Add(item.ToXElement());
            document.Save(GetXmlFilePath(typeof(T)));
        }

        public void AddDroneCharge(int droneId, int baseStationId)
        {
            if (DoesExist<DroneCharge>(charge => charge.DroneId == droneId))
                throw new IdAlreadyExistsException(typeof(DroneCharge), droneId);

            if (!DoesExist<Drone>(d => d.Id == droneId))
                throw new ObjectNotFoundException(typeof(Drone));

            if (!DoesExist<BaseStation>(s => s.Id == baseStationId))
                throw new ObjectNotFoundException(typeof(BaseStation));

            DroneCharge charge = new DroneCharge()
            {
                DroneId = droneId,
                StationId = baseStationId,
                StartTime = DateTime.Now,
                IsDeleted = false,
            };

            XDocument document = XDocument.Load(GetXmlFilePath(typeof(DroneCharge)));
            document.Root.Add(charge.ToXElement());
            document.Save(GetXmlFilePath(typeof(DroneCharge)));
        }

        #endregion

        #region Request

        public T GetById<T>(int id) where T : IIdentifiable, IDeletable
        {
            return GetSingle<T>(item => item.Id == id);
        }

        public T GetSingle<T>(Predicate<T> predicate) where T : IDeletable
        {
            try
            {
                return GetFilteredList(predicate).Single();
            }
            catch (InvalidOperationException e)
            {
                throw new ObjectNotFoundException(typeof(T), e);
            }
        }

        public IEnumerable<T> GetList<T>() where T : IDeletable
        {
            return XDocument.Load(GetXmlFilePath(typeof(T)))
                            .Root
                            .Elements(typeof(T).Name)
                            .Select(xelement => xelement.FromXElement<T>())
                            .Where(item => !item.IsDeleted);
        }

        public IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T : IDeletable
        {
            return GetList<T>().Where(item => predicate(item));
        }

        public int GetParcelContinuousNumber()
        {
            XDocument document = XDocument.Load(ConfigFilePath);
            XElement element = document.Root.Element("NextParcelId");
            int value = int.Parse(element.Value);

            element.SetValue(value + 1);
            document.Save(ConfigFilePath);

            return value;
        }

        public (double, double, double, double, double) GetElectricityConfumctiol()
        {
            XDocument document = XDocument.Load(ConfigFilePath);
            XElement electricityConfumctiol = document.Root.Element("ElectricityConfumctiol");

            return
            (
                double.Parse(electricityConfumctiol.Element("Free").Value),
                double.Parse(electricityConfumctiol.Element("Light").Value),
                double.Parse(electricityConfumctiol.Element("Medium").Value),
                double.Parse(electricityConfumctiol.Element("Heavy").Value),
                double.Parse(document.Root.Element("ChargeRate").Value)
            );
        }
        #endregion

        #region Update

        public void Update<T>(int id, string propName, object newValue) where T : IIdentifiable, IDeletable
        {
            XDocument document = XDocument.Load(GetXmlFilePath(typeof(T)));
            XElement element;

            try
            {
                element = document.Root.Elements()
                                       .Single(xElement => id == int.Parse(xElement.Element(nameof(IIdentifiable.Id)).Value));
            }
            catch (InvalidOperationException)
            {
                throw new ObjectNotFoundException(typeof(T));
            }

            element.Element(propName).RemoveAttributes();
            element.SetElementValue(propName, newValue);

            document.Save(GetXmlFilePath(typeof(T)));
        }

        public void UpdateWhere<T>(Predicate<T> predicate, string propName, object newValue) where T : IDeletable
        {
            XDocument document = XDocument.Load(GetXmlFilePath(typeof(T)));
            document.Root.Elements().AsParallel().ForAll(xElement =>
            {
                if (predicate(xElement.FromXElement<T>()))
                {
                    xElement.Element(propName).RemoveAttributes();
                    xElement.SetElementValue(propName, newValue);
                }
            });

            document.Save(GetXmlFilePath(typeof(T)));
        }

        #endregion

        #region Delete

        public void Delete<T>(int id) where T : IIdentifiable, IDeletable
        {
            Update<T>(id, nameof(IDeletable.IsDeleted), true);
        }

        public void DeleteWhere<T>(Predicate<T> predicate) where T : IDeletable
        {
            UpdateWhere(predicate, nameof(IDeletable.IsDeleted), true);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks whether an item of type T with a given id is exists (and not deleted)
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item id</param>
        /// <returns>true if the item exists otherwise false</returns>
        private bool DoesExist<T>(Predicate<T> predicate) where T : IDeletable
        {
            return GetFilteredList(predicate).Any();
        }

        #endregion

    }
}
