// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Runtime.CompilerServices;
var g = new NameGenerator.Generators.RealNameGenerator();

List<Student> students= new List<Student>();

int[] age = [21,24,13,34,16];
foreach(int ele in age){
    Student student = new Student(g.Generate(),ele);
    students.Add(student);

}
string userInput;
do{
    Console.WriteLine("Enter a command or 'Exit'");
    userInput = Console.ReadLine();
    if(userInput.Length <= 0){
        continue;
    }
    if(userInput[0] == 'l'){
        foreach(Student student in students){
            Console.WriteLine(student.getStudent());
        }
        Console.WriteLine("\n");
    }

    if(userInput[0] == 'd'){
        int count = students.RemoveAll(student=>student.getFirstName() == userInput.Split(" ")[1]);
        Console.WriteLine($"{count} student(s) removed!!");
    }

    if(userInput[0] == 'a'){
        string newName = userInput.Split(" ")[1]+" "+userInput.Split(" ")[2];
        int newAge = Int32.Parse(userInput.Split(" ")[3]);
        Student newStudent = new Student(newName,newAge);
        students.Add(newStudent);
        Console.WriteLine("student added!!");
    }
}
while(userInput != "Exit");

Console.WriteLine("Exitted program!!");

public class Student{
    string name;
    int age;
    public Student(string name,int age){
        this.name = name;
        this.age = age;
    }

    public string getStudent(){
        return $"{this.name} is {this.age} years old";
    }

    public string getFirstName(){
        return this.name.Split(" ")[0];
    }

    public string getLastName(){
        return this.name.Split(" ")[1];
    }

    public int getAge(){
        return this.age;
    }
}
