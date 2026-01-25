using System;

class SortingSys {
    static void Main() {
        Console.WriteLine("Hello World");

        Sorter s1 = new Sorter(new int[] {33, 4, 4536, 234, 534, 5, 125, 6712, 5});
        s1.QuickSort(); 

        
        Console.WriteLine("Sorted Array:");
        foreach (int n in s1.List) {
            Console.Write(n + " ");
        }
    }

    class Sorter {
        public int[] List; 

        public Sorter(int[] n) {
            List = n;
        }
        public void QuickSort(){
            QuickRecursice(0,this.List.Length - 1);
        }
        public void QuickRecursice(int left, int right) {
            if (left >= right)
                return;

            int l = left;
            int r = right;
            int pivot = List[(left + right) / 2];

            while (l <= r) {
                while (List[l] < pivot)
                    l++;
                while (List[r] > pivot)
                    r--;

                if (l <= r) {
                    int temp = List[l];
                    List[l] = List[r];
                    List[r] = temp;
                    l++;
                    r--;
                }
            }

            QuickRecursice(left, r);
            QuickRecursice(l, right);
        }
    }
}
