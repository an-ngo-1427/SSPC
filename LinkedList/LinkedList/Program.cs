// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Runtime.Serialization;

LinkedList newList = new LinkedList();
newList.addAllNode([1,3,5,6]);
newList.printList();
Console.WriteLine("list length" + newList.Length);


public class LinkedList{
    public Node Head ;
    public int Length;
    public LinkedList(){
        this.Head = null;
        this.Length = 0;
    }

    public void addNode(int value){
        Node newNode = new(value);
        if(this.Head == null){
            this.Head = newNode;
        }else{
            Node temp = this.Head;
            while(temp.Next != null){
                temp = temp.Next;
            }
            temp.Next = newNode;
        }
        this.Length++;
    }

    public void insertSorted(Node node){
        Node prev = this.Head;
        Node curr = this.Head;
        if(this.Head == null){
            this.Head = node;
        }else{
            while(curr != null){
                curr = curr.Next;
                if(node.Value < curr.Value && prev.Value < node.Value){
                    prev.Next = node;
                    node.Next = curr;
                    break;
                }else{
                    prev = curr;
                    curr = curr.Next;
                }
            }
        }
    }

    public void addAllNode(int[] values){
        foreach(int value in values){
            this.addNode(value);
        }
    }

    public void printList(){
        Node temp = this.Head;
        while(temp != null){
            Console.WriteLine(temp.Value);
            temp = temp.Next;
        }
    }
}

public class Node{
    public int Value;
    public Node Next;
    public Node(int value){
        this.Value = value;
        this.Next = null;
    }
}
