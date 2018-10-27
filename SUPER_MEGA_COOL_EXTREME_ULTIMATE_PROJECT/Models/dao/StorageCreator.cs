using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.dao
{
    public interface StorageCreator
    {
        void createStorageFolder(String path);       

        void createStorage();

        void clearStorage();
    }
}
