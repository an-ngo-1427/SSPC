// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
public class LFUCache {
    class Data
    {
        public int key;
        public int val;
        public int freq;
    }

    int _capacity;
    LinkedList<Data> _list = new LinkedList<Data>();
    Dictionary<int, LinkedListNode<Data>> _keyToNode = new Dictionary<int, LinkedListNode<Data>>();

    public LFUCache(int capacity) {
        _capacity = capacity;
    }

    public int Get(int key) {
        if(!_keyToNode.ContainsKey(key))
            return -1;

        var node = _keyToNode[key];
        node.Value.freq++;
        _list.Remove(node);
        _list.AddLast(node);

        return node.Value.val;
    }

    public void Put(int key, int value)
    {
        if(_keyToNode.ContainsKey(key))
        {
            var node = _keyToNode[key];
            node.Value.freq++;
            node.Value.val = value;
            _list.Remove(node);
            _list.AddLast(node);
        }
        else
        {
            if(_list.Count > 0 && _list.Count == _capacity)
            {
                var nodeToRemove = _list.First;
                _list.Remove(nodeToRemove);
                _keyToNode.Remove(nodeToRemove.Value.key);
            }

            var newNodeData = new Data { key = key, val = value, freq = 1 };
            var newNode = _list.AddLast(newNodeData);
            _keyToNode[key] = newNode;
        }
    }
}
public class Solution {
    static void Main() {
        var cache1 = new LFUCache(0);
        Console.WriteLine(cache1.Get(1));
        cache1.Put(1, 123);
        Console.WriteLine(cache1.Get(1));
        var cache2 = new LFUCache(3);
        cache2.Put(2, 2);
        cache2.Put(1, 1);
        Console.WriteLine(cache2.Get(2));   // should print 2
        Console.WriteLine(cache2.Get(1));   // should print 1
        Console.WriteLine(cache2.Get(2));   // should print 2
        cache2.Put(3, 3);
        cache2.Put(4, 4);
        Console.WriteLine(cache2.Get(3));   // should print -1
        Console.WriteLine(cache2.Get(2));   // should print 2
        Console.WriteLine(cache2.Get(1));   // should print 1
        Console.WriteLine(cache2.Get(4));   // should print 4
    }
}
