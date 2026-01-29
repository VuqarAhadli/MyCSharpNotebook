using System;

class BinarySearch
{
    static void Main()
    {
        int[] data = { 2, 41, 413, 4, 32, 52, 3, 523, 43, 334244, 3245, 235, 4287, 89, 56, 49, 853 };

        BinarySearcher bs = new BinarySearcher(data, 49);
        bs.QuickSort();
        int index = bs.Search();

        if (index != -1)
            Console.WriteLine($"Target found at index {index}");
        else
            Console.WriteLine("Target not found");
    }
}

class BinarySearcher
{
    private int[] list;
    private int target;
    private int length;

    public BinarySearcher(int[] l, int t)
    {
        list = l;
        target = t;
        length = list.Length;
    }

    public void QuickSort()
    {
        QuickRecursive(0, length - 1);
    }

    private void QuickRecursive(int left, int right)
    {
        if (left >= right)
            return;

        int l = left;
        int r = right;
        int pivot = list[(left + right) / 2];

        while (l <= r)
        {
            while (list[l] < pivot) l++;
            while (list[r] > pivot) r--;

            if (l <= r)
            {
                int temp = list[l];
                list[l] = list[r];
                list[r] = temp;
                l++;
                r--;
            }
        }

        QuickRecursive(left, r);
        QuickRecursive(l, right);
    }
     
    public int Search()
    {
        int left = 0;
        int right = length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (list[mid] == target)
                return mid;
            else if (list[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }
}
