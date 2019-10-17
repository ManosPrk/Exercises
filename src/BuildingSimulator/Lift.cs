using System;
using System.Linq;
using System.Collections.Generic;

namespace BuildingSimulator
{

    public class Lift
    {
        List<Visitor> Visitors { get {return Visitors;} set {} }

        ILocation currentFloor { get{ return LocationFactory.GetLocation(floorNumber); } }
        private int maxCapacity;
        public int CurrentCapacity { get {return Visitors.Count;} }
        readonly int cycles;
        public int MaxCapacity { get {return maxCapacity;} }
        public int floorNumber = 0;
        public bool IsThereSpace { get {if (CurrentCapacity < maxCapacity) return true; else return false;}}
        public Lift()
        {
            var capacities = Capacities.Instance();
            maxCapacity = 100;
            cycles = 10;
        }

        public void Operate()
        {
            while (cycles > 0)
            {
                // moveUp();
                // moveDown();       
            }
        }



        public void Enter(Visitor visitor)
        {
            Visitors.Add(visitor);
        }
        
        public Visitor Exit()
        {
            Visitor visitor = Visitors.First();
            Visitors.Remove(visitor);
            return visitor;
        }

        public void moveUp()
        {
            while (currentFloor != null)
            {
                while (IsThereSpace && currentFloor.CurrentCapacity != 0)
                {
                    Enter(currentFloor.Exit());
                }

                while (currentFloor.IsThereSpace && CurrentCapacity != 0)
                {
                    currentFloor.Enter(Exit());
                }
                floorNumber++;
            }
        }

        public void moveDown()
        {
            floorNumber--;
            while (floorNumber > 0)
            {
                while (IsThereSpace)
                {
                    Enter(currentFloor.Exit());
                }

                while (currentFloor.IsThereSpace)
                {
                    currentFloor.Enter(Exit());
                }
                floorNumber--;
            }
        }
    }
}