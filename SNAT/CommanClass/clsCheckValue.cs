using System;
using SNAT.Comman_Classes;
public static class  clsCheckValue
{
    public enum _enmValidationCulture
    {
        cString,
        iNumeric,
        iInteger,
        rDecimal,
        rDouble,
        lBoolean,
        dDate
    }

    public static bool CheckValue(object strValue, ref object rValue, _enmValidationCulture _enmValidationCulture = _enmValidationCulture.cString, bool ZeroAsNull = false)
    {
        int riValue = 0;
        decimal rdcValue = 0;
        double rdoValue = 0;
        bool lResult = false;

        try
        {



            if (strValue == null)
                return true;
            switch (_enmValidationCulture)
            {

                case _enmValidationCulture.cString:
                    if (strValue == null) { rValue = ""; lResult = true; break; }
                    if (string.IsNullOrEmpty(strValue.ToString())) { rValue = ""; lResult = true; break; }
                    if (string.IsNullOrWhiteSpace(strValue.ToString())) { rValue = ""; lResult = true; break; }
                    rValue = strValue.ToString().Trim();

                    lResult = false; break; ;
                case _enmValidationCulture.dDate:
                    if (strValue == null) { lResult = true; break; }

                    if (string.IsNullOrEmpty(strValue.ToString()))
                    { lResult = true; break; }
                    if (string.IsNullOrWhiteSpace(strValue.ToString()))
                    { lResult = true; break; }
                    DateTime rdValue;
                    DateTime.TryParse(strValue.ToString(), out rdValue);
                    if (rdValue.ToString() == ("12:00 AM"))
                    {
                        rValue = null;
                        lResult = true; break;
                    }
                    else
                    {
                        rValue = rdValue;
                        lResult = false; break;


                    }

                case _enmValidationCulture.iInteger:
                    if (strValue == null) { rValue = 0; lResult = true; break; }
                    if (string.IsNullOrEmpty(strValue.ToString())) { rValue = 0; lResult = true; break; }
                    if (string.IsNullOrWhiteSpace(strValue.ToString())) { rValue = 0; lResult = true; break; }

                    if (ZeroAsNull == true)
                    {
                        int.TryParse(strValue.ToString(), out riValue);
                        if (riValue == 0)
                        {
                            rValue = 0;
                            lResult = true; break;
                        }
                        else
                        {
                            rValue = riValue;
                            lResult = false; break;
                        }
                    }
                    else
                    {

                        lResult = int.TryParse(strValue.ToString(), out riValue);
                        rValue = riValue;
                        return lResult;

                    }


                case _enmValidationCulture.rDecimal:
                    if (strValue == null) { rValue = 0; return true; }
                    if (string.IsNullOrEmpty(strValue.ToString())) { rValue = 0; return true; }
                    if (string.IsNullOrWhiteSpace(strValue.ToString())) { rValue = 0; return true; }

                    if (ZeroAsNull == true)
                    {
                        decimal.TryParse(strValue.ToString(), out rdcValue);
                        if (rdcValue == 0)
                        {
                            rValue = 0;
                            lResult = true; break;
                        }
                        else {
                            rValue = rdcValue;
                            lResult = false; break;
                        }
                    }
                    else
                    {
                        lResult = decimal.TryParse(strValue.ToString(), out rdcValue);
                        rValue = rdcValue;
                        return lResult;
                    }


                case _enmValidationCulture.rDouble:
                    if (strValue == null) { rValue = 0; return true; }
                    if (string.IsNullOrEmpty(strValue.ToString())) { rValue = 0; return true; }
                    if (string.IsNullOrWhiteSpace(strValue.ToString())) { rValue = 0; return true; }

                    if (ZeroAsNull == true)
                    {
                        double.TryParse(strValue.ToString(), out rdoValue);
                        if (Math.Abs(rdoValue) <=0)
                        {
                            rValue = rdoValue;
                            lResult = true; break;
                        }
                        else
                        {
                            rValue = rdoValue;
                            lResult = false; break;
                        }
                    }
                    else
                    {
                        lResult = double.TryParse(strValue.ToString(), out rdoValue);
                        rValue = rdoValue;
                        return lResult;
                    }

                case _enmValidationCulture.lBoolean:
                    if (strValue == null) { rValue = false; return true; }
                    if (string.IsNullOrEmpty(strValue.ToString())) { rValue = false; return true; }
                    if (string.IsNullOrWhiteSpace(strValue.ToString())) { rValue = false; return true; }
                    if (bool.TryParse(strValue.ToString(), out lResult) == true)
                    {
                        rValue = lResult;
                        lResult = false; break;
                    }
                    else
                    {
                        rValue = lResult;
                        lResult = true; break;
                    }


            }
            return lResult;
        }
        catch (Exception ex)
        {
            ClsMessage.ProjectExceptionMessage(ex);

            return false;
        }


    }

}




