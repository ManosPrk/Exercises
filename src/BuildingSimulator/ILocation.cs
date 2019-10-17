using System;
using System.Collections.Generic;

namespace BuildingSimulator
{
    public interface ILocation
    {
        List<Visitor> Visitors { get; set; }
        int CurrentCapacity { get; }
        int MaxCapacity { get; }

        bool IsThereSpace { get; set; }

        Visitor Exit();
        void Enter(Visitor visitor);
    }    
}