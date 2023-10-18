The Service Processing Application must use two Queue<T> data structures of a simple class which has the following five attributes: 
Client Name, Drone Model, Service Problem, Service Cost and Service Tag. 

When a client delivers a drone to Icarus for attention the front desk staff will enter the details as required to populate the Drone class.
The client will be able to select between a regular or express priority for the service of their drone. The express priority service will incur
an additional 15% charge to the service cost. Once the priority has been selected the drone will be added to one of the two queues (regular or express).
The client’s drone will be tagged and send to the service department for inspection and repair/service. Once the drone is repaired and returned to the 
front desk the Icarus staff will remove the details from the queue. This removal process will dequeue the appropriate data structure and add the details 
to the list of completed work. Finally, the client will be able to pay for the work and collect their drone; the staff on the front desk will then remove 
the item from the finished work list. 

All user interactions must have full error trapping and feedback messaging which is displayed in a status strip at the bottom of the form. The need to use 
a message box for critical errors or general feedback is not necessary. 
