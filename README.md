Most data ser/deser solutions consider the entire object, while sometimes data contracts should represent partial updates.

usage:
```
//create a contract interface, inheriting from IJsonGeneratable

var generatorFactory = new JsonGeneratorFactory();
ISomeContract generator = generatorFactory.CreateJsonGenerator<ISomeContract>();

//update some stuff on generator
generator.SomeProp = "someValue";
...
//get the json 
generator.GetJson();

```

This create a json containing only updates on some data contract.