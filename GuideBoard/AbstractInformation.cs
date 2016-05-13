using System.Collections.Generic;
using System.Drawing;

namespace GuideBoard
{
    class AbstractInformation
    {
         public ContextInfromation[] GetContextInfromation(int[] commandTemp,string[] strTemp)
         {
             ContextInfromation[] contextInfromationTemp=new ContextInfromation[strTemp.Length];
             for (int i = 0; i < strTemp.Length; i++)
             {
                 if (commandTemp[i] == 1)
                 {
                     contextInfromationTemp[i] = null;

                 }
                 else
                 {                     
                     string[] strSplit = strTemp[i].Split(new char[3] { '[', '{', '}' });

                 }

             }
             return null;
         }
    }
   //  public delegate ContextInfromation[] AsyncMethodCaller(string[] strTemp);
}