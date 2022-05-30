using UnityEngine;

namespace BreadAndButter
{
    public class SceneFieldAttribute : PropertyAttribute
    {
        //Function will take in file path and turn it into a file that can be loaded
        //Default path will include asset file path/.unity (THIS WILL CAUSE PROBLEMS)
        //So create helper function that allows for custom editor to load name

        public static string LoadableName(string _path)
        {
            //These are variables which we want ignored from path
            string start = "Assets/";
            string end = ".unity";

            //If path starts with Assets/Scenes/test.unity (EG)
            if(_path.StartsWith(start))
            {
                //Change path name
                _path = _path.Substring(start.Length);
            }

            //Repeat for end (.unity)
            if(_path.EndsWith(end))
            {
                //If path contains 'end' data remove it
                _path = _path.Substring(0, _path.LastIndexOf(end));
            }

            //Return newly edited path
            return _path;
        }
    }
}
