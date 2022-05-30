using System;

public class Temperature : IComparable
{

    /// <returns>
    /// Less than zero         - The current instance precedes the object specified by the CompareTo method in the sort order.
    /// Zero                   - This current instance occurs in the same position in the sort order as the object specified by the CompareTo method.
    /// Greater than zero	   - This current instance follows the object specified by the CompareTo method in the sort order.
    /// </returns>
    public int CompareTo(object obj)
    {
        if(obj == null)
            //This should be after the passed object = 'hotter' temp object
            return 1;

        //Write out comparator
        Temperature otherTemp = obj as Temperature;

        if(otherTemp != null)
        {
            //Now we can compare them
            if(this.temperature > otherTemp.temperature)
            {
                //It should come after in the list as it is 'hotter' than the passed object
                return 1;
            }
            else if(this.temperature < otherTemp.temperature)
            {
                //This should come before it in the list as it is 'colder' than the passed object
                return -1;
            }

            //The temperatures are the same so return 0
            return 0;
        }
        else
        {
            //We couldn't cast the object to a temperature so throw an exception
            throw new ArgumentException("Object is NOT a temperature");
        }
    }

    protected float temperature;

    public Temperature(float _temp)
    {
        temperature = _temp; //Need something to compare to 
    }

    public override string ToString()
    {
        return temperature.ToString("0.0"); //Makes decimal points in string
    }
}
