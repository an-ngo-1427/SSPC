class MyCircularQueue(object):
    def __init__(self, k):
        """
        :type k: int
        """
        self.size = k
        self.length = 0
        self.array = []
        self.firstIdx = None
    def enQueue(self, value):
        """
        :type value: int
        :rtype: bool
        """

        if(self.length < self.size):
            self.array.append(value)
            if(self.length == 0):
                self.firstIdx = 0
            self.length += 1
            return True
        return False

    def deQueue(self):
        """
        :rtype: bool
        """
        if(self.length <= 0):
            return False

        print(self.array[self.firstIdx])
        if(self.length > 1):
            self.firstIdx +=1
        else:
            self.firstIdx = None
        self.length -= 1
        return True

    def Front(self):
        """
        :rtype: int
        """
        if(self.length == 0):
            return -1

        return self.array[self.firstIdx]

    def Rear(self):
        """
        :rtype: int
        """
        if(self.length == 0):
            return -1
        return self.array[len(self.array)-1]

    def isEmpty(self):
        """
        :rtype: bool
        """
        return self.length == 0

    def isFull(self):
        """
        :rtype: bool
        """
        return self.length == self.size
