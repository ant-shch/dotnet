### Copy object reference to stack
* when you copy reference object to stack it reference connect to heap but located in stack (``` var animal = Animal.Info;```)
  when you initialize new reference object on stack variable ```animal = new AnimalInfo(5);```, reference rewrited on stack but value doesn't change in heap;
