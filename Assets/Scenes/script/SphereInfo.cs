using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInfo : MonoBehaviour
{
    public string file = null;
    public int modele = 0;

    private List<List<string>> lines;
    private List<string> files;
    private const int BS = 0;
    private const int CARACTERISTIQUE = 1;
    private const int CORVAR = 2;
    private const int COS2 = 3;
    private const int COS2VAR = 4;
    private const int CTR = 5;
    private const int CTRVAR = 6;
    private const int DI = 7;
    private const int EIGVAL = 8;
    private const int EXPLAINED = 9;
    private const int SECTEURS = 10;

    // Start is called before the first frame update
    void Start()
    {
        //ChargementDonnees(file, modele);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChargementDonnees(string file, int modeleFile)
    {
        lines = new List<List<string>>();

        string line;

        files.Add(file + "bs.txt");
        files.Add(file + "caracteristique.txt");
        files.Add(file + "corvar.txt");
        files.Add(file + "cos2.txt");
        files.Add(file + "cos2var.txt");
        files.Add(file + "ctr.txt");
        files.Add(file + "ctrvar.txt");
        files.Add(file + "di.txt");
        files.Add(file + "eigval.txt");
        files.Add(file + "explained_variance_ratio_.txt");
        files.Add(file + "secteurs.txt");

        for (int i = 0; i < files.Count; i++)
        {
            lines[i] = new List<string>();

            System.IO.StreamReader fileT = new System.IO.StreamReader(files[i]);

            while ((line = fileT.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                lines[i].Add(line);
            }
        }
    }
}
