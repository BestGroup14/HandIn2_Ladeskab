using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public interface IDoor
    {
        void OpenDoor();

        void CloseDoor();

        void LockDoor();

        void UnlockDoor();
    }
}
