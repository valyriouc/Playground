
public class ListNode
{
      public int val;
      public ListNode next;
      public ListNode(int val = 0, ListNode next = null)
       {
        this.val = val;
        this.next = next;
      }

      public void Print()
    {
        Console.WriteLine(val);
        next?.Print();
    }
  }

public class Solution
{
    public static void Main()
    {
        ListNode node4 = new ListNode(4);
        ListNode node3 = new ListNode(3, node4);
        ListNode node2 = new ListNode(2, node3);
        ListNode head = new ListNode(1, node2);

        ListNode first = SwapPairs(head);

        first.Print();

    }

    public static ListNode SwapPairs(ListNode head)
    {
        ListNode next = head.next;
        ListNode tree = next?.next ?? null;
        ListNode current = head;

        if (tree != null)
        {
            tree = SwapPairs(tree);
        }

        current.next = tree;
        next.next = current;

        return next;
    }
}