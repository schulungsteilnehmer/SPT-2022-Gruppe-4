/*
 * Created by SharpDevelop.
 * User: schulung
 * Date: 26.04.2022
 * Time: 11:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zeugnisse
{
	class Program
	{
	
		public static void Main(string[] args)
		{
			string name = " ";
			DateTime datum = new DateTime{};
			List<string> fächer = new List<string>{};
			List<int> noten = new List<int>{};
			
			bool nameWrong = true;
			Console.WriteLine("Bitte geben Sie den Namen des Schühlers ein (z.B. Lasse Schmidt)");
			while (nameWrong)
			{				
				name = Console.ReadLine();
				nameWrong = false;
				for (int i = 0; i < name.Length; i++) {
					if (!(name[i] == ' ' || name[i] == '-' || Char.IsLetter(name[i])))
					{
						Console.WriteLine("Bitte nur gültige Namen eingeben!");
						nameWrong = true;
						break;
					}
				}
				
			}
			
			Console.WriteLine("Bitte geben Sie das Zeugnisausstellungsdatum ein (z.B. 14.03.2024)");
			while (!DateTime.TryParse(Console.ReadLine(), out datum))
			{
				Console.WriteLine("Bitte Datum in gültigem Format (dd.MM.yyyy) eingeben!");
			}				
			
			for (int i = 0; i < 8; i++) {
				// string illegalChars = "!@#$%^&*()@";
				if(i==0)Console.WriteLine("Bitte geben Sie den Leistungkurs 1 ein.(z.B. Mathe)");
				else if(i==1) Console.WriteLine("Bitte geben Sie den Leistungskurs 2 ein. (z.B. Deutsch)");
				else Console.WriteLine("Bitte geben Sie einen Kurs ein. (z.B. Englisch)");
				string input = Console.ReadLine();
				fächer.Add(input);
				
				Console.WriteLine("Bitte geben die Note (0-15) für das Fach {0} ein.",input);
				int note = -1;
				while (!Int32.TryParse(Console.ReadLine(), out note) || !((note <= 15) && (note >= 0)))
				{
					Console.WriteLine("Bitte Note von 0-15 eingeben!");
				}	
				noten.Add(note);
			}
			
			Console.WriteLine("Bitte geben Sie die Fehltage des Schühlers ein (z.B.31)");
			int fehltage = Int32.Parse(Console.ReadLine());
			
			Console.WriteLine("Bitte geben Sie die unentschuldigten Fehltage des Schühlers ein (z.B.31)");
			int unentschuldigteFehltage = Int32.Parse(Console.ReadLine());
			
			
			
			
			Console.WriteLine("\n\n" + name + " \n" + datum + "\n");
			for (int i = 0; i < 8; i++) {
				Console.WriteLine(fächer[i] + "\t" + noten[i]);
			}
			Console.WriteLine("Fehltage: " + fehltage);
			Console.WriteLine("Davon unentschuldigt: " + unentschuldigteFehltage);
			bool versetzt = Versetzung(noten, unentschuldigteFehltage);
			if (versetzt)
			{
				Console.WriteLine("Schüler wird versetzt.");
			} else 
				Console.WriteLine("Schüler wird nicht versetzt.");
			Console.WriteLine("Möchten Sie das Zeugnis als Datei speichern? Y/N");
			char choice = Console.ReadLine()[0];
			if (choice == 'y' || choice =='Y') ZeugnisAusgeben(name, datum, fächer, noten, fehltage, unentschuldigteFehltage, versetzt);
		}
		
		public static void ZeugnisAusgeben(string name, DateTime datum, List<string> fächer, List<int> noten, int fehltage, int unentschuldigteFehltage, bool versetzt)
			{
			string path = "C:\\Users\\schulung.SCHULUNGNB-03\\Documents\\SPT-2022-Gruppe-4\\Zeugnis.txt";
			StreamWriter writer = new StreamWriter(path);
			writer.WriteLine("\n\n" + name + " \n" + datum.ToString() + "\n");
			for (int i = 0; i < 2; i++) {
				writer.WriteLine(fächer[i] + "(LK) \t" + noten[i]);
			}
			for (int i = 2; i < 8; i++) {
				writer.WriteLine(fächer[i] + "\t" + noten[i]);
			}
			writer.WriteLine("Fehltage: " + fehltage);
			writer.WriteLine("Davon unentschuldigt: " + unentschuldigteFehltage);
			if (versetzt)
			{
				writer.WriteLine("Schüler wird versetzt.");
			} else 
				writer.WriteLine("Schüler wird nicht versetzt.");
			writer.Close();
		}
		
		public static double DurchschnittBerechnen(List<int> noten)
			{
			double durchschnitt = 0;
			for (int i = 0; i < 2; i++) {
				if(noten[i] == 0) durchschnitt += 12;
				else if(noten[i] == 15) durchschnitt += 2;
				else durchschnitt += ((17-noten[i])/3*2);
			}
			for (int i = 2; i < noten.Count; i++) {
				if(noten[i] == 0) durchschnitt += 6;
				else if(noten[i] == 15) durchschnitt += 1;
				else durchschnitt += ((17-noten[i])/3);
			}
			durchschnitt /= 10;
			durchschnitt = Math.Round(durchschnitt, 1);
			return durchschnitt;
		}
		public static bool Versetzung(List<int> noten, int unentschuldigteFehltage)
		{
			int zähler = 0;
			for (int i = 0; i < noten.Count; i++) {
				if (noten[i] < 5) zähler++;
			}
			if (zähler <= 2 && unentschuldigteFehltage < 30) return true;
			else return false;
		}
	}
}
