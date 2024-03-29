using System.Collections.Generic;
using System.Linq;

namespace BuildingSimulator
{
    public class Lift
    {
        public List<Visitor> Visitors { get; set; }
        ILocation currentFloor { get { return LocationSingletory.GetLocation(floorNumber); } }
        private int maxCapacity;
        public int CurrentCapacity { get { return Visitors.Count; } }
        private int cycles;
        public int MaxCapacity { get { return maxCapacity; } }
        public int floorNumber;
        public bool IsThereSpace { get { if (CurrentCapacity < maxCapacity) return true; else return false; } }
        public Lift()
        {
            Visitors = new List<Visitor>();
            maxCapacity = Capacities.Get("Lift");
            cycles = Capacities.Get("Cycles");
        }

        public void Operate()
        {
            while (cycles > 0)
            {
                moveUp();
                moveDown();
                cycles--;
            }
        }

        public void Enter(Visitor visitor)
        {
            Visitors.Add(visitor);
        }

        public Visitor Exit()
        {
            Visitor visitor;
            if (currentFloor is GroundFloor)
            {
                visitor = Visitors.Where(x => x.Served).FirstOrDefault();
            }
            else
            {
                visitor = Visitors.Where(x => x.FloorNumber == floorNumber).FirstOrDefault();
            }
            Visitors.Remove(visitor);
            return visitor;
        }

        private void moveUp()
        {

            while (currentFloor != null)
            {
                if (currentFloor is GroundFloor)
                {
                    while (IsThereSpace && currentFloor.CurrentCapacity > 0)
                    {
                        Enter(currentFloor.Exit());
                    }
                }
                else
                {
                    while (currentFloor.IsThereSpace && CurrentCapacity != 0)
                    {
                        var visitor = Exit();
                        if (visitor == null)
                        {
                            break;
                        }
                        else
                        {
                            currentFloor.Enter(visitor);
                        }

                    }
                }
                floorNumber++;
            }
            floorNumber--;
        }

        private void moveDown()
        {
            while (currentFloor != null)
            {
                if (currentFloor is GroundFloor)
                {
                    while (CurrentCapacity > 0)
                    {
                        Visitor visitor = Exit();
                        if (visitor == null)
                        {
                            break;
                        }
                        else
                        {
                            currentFloor.Enter(visitor);
                        }
                    }
                }
                else
                {
                    while (IsThereSpace && currentFloor.CurrentCapacity > 0)
                    {
                        Visitor visitor = currentFloor.Exit();
                        if (visitor == null)
                        {
                            break;
                        }
                        else
                        {
                            Enter(visitor);
                        }
                    }
                }
                floorNumber--;
            }
            floorNumber++;
        }
    }
}