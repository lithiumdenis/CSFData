using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Анализатор
{
    public enum ElementType
    {
        None = 1,
        Variable = 2,
        OfficialWord = 3,
        Cycle = 4,
        BaseType = 5,
        Operation = 6
    }

    public class ElmentLanguage
    {
        public String Name { get; set; }
        private ElementType type;
        private int count;

        public ElmentLanguage()
        {
            this.Name = "";
            this.type = ElementType.None;
            count = 0;
        }

        public ElmentLanguage(String name)
        {
            this.Name = name;
            this.type = ElementType.None;
            count = 0;
        }

        public void SetType(ElementType t)
        {
            this.type = t;
        }

        public ElementType GetType()
        {
            return type;
        }

        public void IncCount()
        {
            this.count++;
        }

        public int Count
        {
            get { return count; }
        }
    }
}
