using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberToString
{
    public static string ChangeNumberToString(long number)
    {
        string a = number.ToString();

        int b = a.Length;

        switch (b)
        {
            case 0:
                return "";
            case 1:
                return a;
            case 2:
                return a;
            case 3:
                return a;
            case 4:
                return a;
            case 5:
                return a;

            case 6:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + a[1].ToString() + a[2].ToString() + "." + a[3].ToString() + "K";
                }
                else
                {
                    return a[0].ToString() + a[1].ToString() + a[2].ToString() + "K";
                }

            case 7:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + "." + a[1].ToString() + "M" ;
                }
                else
                {
                    return a[0].ToString() + "M";
                }

            case 8:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + a[1].ToString() + "." + a[2].ToString() + "M";
                }
                else
                {
                    return a[0].ToString() + a[1].ToString() + "M";
                }

            case 9:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + a[1].ToString() + a[2].ToString() + "." + a[3].ToString() + "M";
                }
                else
                {
                    return a[0].ToString() + a[1].ToString() + a[2].ToString() + "M";
                }

            case 10:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + "." + a[1].ToString() + "B";
                }
                else
                {
                    return a[0].ToString() + "B";
                }

            case 11:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + a[1].ToString() + "." + a[2].ToString() + "B";
                }
                else
                {
                    return a[0].ToString() + a[1].ToString() + "B";
                }

            case 12:

                if (System.Int64.Parse(a[2].ToString()) != 0)
                {
                    return a[0].ToString() + a[1].ToString() + a[2].ToString() + "." + a[3].ToString() + "B";
                }
                else
                {
                    return a[0].ToString() + a[1].ToString() + a[2].ToString() + "B";
                }

            default:
                return a;
        }
    }
}
