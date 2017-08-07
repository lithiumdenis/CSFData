using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Анализатор
{
    public class AnalysisCode
    {
        private List<ElmentLanguage> elements;

        public AnalysisCode()
        {
            elements = new List<ElmentLanguage>();
        }

        public void Analysis(String[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                this.AnalysisString(strings[i]);
            this.CleenNotNeed();
        }

        private void AddElem(String str, ElementType type)
        {
            ElmentLanguage el = new ElmentLanguage();
            el.Name = str;
            el.SetType(type);
            el.IncCount();
            this.elements.Add(el);
        }

        private void AnalysisString(String str)
        {
            Char[] mask = new Char[] { ' ', ';', ',', '.', '[', ']', '#' };
            String[] masElem = str.Split(mask);
            // чекаем сравнивая с тем что есть
            for (int i = 0; i < masElem.Length; i++)
                if (!CheackInList(masElem[i]))
                {
                    // такого элемента еще нету
                    // проверка на базовый тип
                    if (this.IsBasisType(masElem[i]))
                    {
                        AddElem(masElem[i], ElementType.BaseType);
                    }
                    else
                    if (this.IsCicle(masElem[i]))
                    {
                        AddElem(masElem[i], ElementType.Cycle);
                    }
                    else
                    if (this.IsOfficialWord(masElem[i]))
                    {
                        AddElem(masElem[i], ElementType.OfficialWord);
                    }
                    if (this.IsOperation(masElem[i]))
                    {
                        AddElem(masElem[i], ElementType.Operation);
                    }
                    else
                        // здесь если предидущии это базовыи тип или слово но не чтото другое то это переменная
                        if (i > 0)
                        if ((IsBasisType(masElem[i - 1]) || IsWord(masElem[i - 1])) && !(IsOfficialWord(masElem[i - 1]) || IsCicle(masElem[i - 1])))
                        {
                            if (IsVarable(masElem[i]))
                                // значит переменная
                                AddElem(masElem[i], ElementType.Variable);
                        }

                }
        }

        private void CleenNotNeed()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Name == "")
                {
                    elements.RemoveAt(i);
                    break;
                }
            }
        }

        public List<ElmentLanguage> GetListElements()
        {
            return elements;
        }

        public List<ElmentLanguage> GetListElementsFor(ElementType type)
        {
            List<ElmentLanguage> list = new List<ElmentLanguage>();
            foreach (ElmentLanguage el in this.elements)
                if (el.GetType() == type)
                    list.Add(el);
            return list;
        }

        private bool CheackInList(String str)
        {
            foreach (ElmentLanguage el in elements)
            {
                if (str == el.Name)
                {
                    el.IncCount();
                    return true;
                }
            }
            return false;
        }



        private bool IsBasisType(String str)
        {
            if (str == "bool" || str == "int" || str == "double" || str == "char" || str == "byte" || str == "string" || str == "void" ||
                str == "bool[]" || str == "int[]" || str == "double[]" || str == "char[]" || str == "byte[]" || str == "String[]" ||
                str == "bool*" || str == "int*" || str == "double*" || str == "char*" || str == "byte*" ||
                str == "bool**" || str == "int**" || str == "double**" || str == "char**" || str == "byte**")
                return true;
            return false;
        }

        private bool IsCicle(String str)
        {
            if (str == "for" || str == "do" || str == "while" || str == "foreach")
                return true;
            return false;
        }

        private bool IsOfficialWord(String str)
        {
            if (str == "new" || str == "public" || str == "private" || str == "protected" ||
                str == "return" || str == "true" || str == "false" || str == "if" ||
                str == "class" || str == "enum" || str == "static" || str == "abstract" ||
                str == "else" || str == "try" || str == "catch" || str == "finally" ||
                str == "null" || str == "{" || str == "}" || str == "using" || str == "namespace" ||
                str == "break" || str == "include" || str == "malloc" || str == "sizeof")
                return true;
            return false;
        }

        private bool IsOperation(String str)
        {
            if (str == "+" || str == "++" || str == "-" || str == "--" ||
                str == "*" || str == "/" || str == "%" || str == "==" ||
                str == "+=" || str == "-=" || str == "*=" || str == "/=" ||
                str == "!=" || str == "!" || str == "=" || str == ">" || str == "<" ||
                str == "<=" || str == ">=" || str == "||" || str == "&&")
                return true;
            return false;
        }

        private bool IsWord(String str)
        {
            Regex r = new Regex(@"\w");
            if (r.IsMatch(str))
                return true;
            return false;
        }

        private bool IsVarable(String str)
        {
            Regex rd = new Regex(@"\w+\d");
            Regex r = new Regex(@"\w");
            if ((r.IsMatch(str) || rd.IsMatch(str)) && CheackVarableWord(str))
                return true;
            return false;
        }

        private bool CheackVarableWord(String str)
        {
            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] == '(' || chars[i] == ')' || chars[i] == '=' || chars[i] == '/')
                    return false;

            return true;
        }

    }
}
