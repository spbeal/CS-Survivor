using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*class Floor
{

}
class FloorDirector
{

}
class GrassFloorBuilder : public Builder
{

}
class Wall
{

}
class Builder
{

}
class WallDirector
{

}
class WoodWallBuilder : public Builder
{

}
class BunkerWallBuilder : public Builder
{

}

//----------------------------------------------------
// Product
class Computer
{
*//*    private:
    string cpu_;
    string ram_;
    string storage_;


    public:
    void setCPU(const std::string& cpu) {
        cpu_ = cpu;
    }

    void setRAM(const std::string& ram)
    {
        ram_ = ram;
    }

    void setStorage(const std::string& storage)
    {
        storage_ = storage;
    }

    void displayInfo() const {
        std::cout << "Computer Configuration:"
                  << "\nCPU: " << cpu_
                  << "\nRAM: " << ram_
                  << "\nStorage: " << storage_ << "\n\n";
    }*//*
};

// Builder interface
class testBuilder
{
*//*    public:
    virtual void buildCPU() = 0;
    virtual void buildRAM() = 0;
    virtual void buildStorage() = 0;
    virtual Computer getResult() = 0;*//*
};

// Director
class ComputerDirector
{
*//*    public:
    void construct(Builder& builder)
    {
        builder.buildCPU();
        builder.buildRAM();
        builder.buildStorage();
    }*//*
};

// ConcreteBuilder
class GamingComputerBuilder : public testBuilder
{
   *//* private:
        Computer computer_;

    public:
        void buildCPU() override
    {
        computer_.setCPU("Gaming CPU");
    }

    void buildRAM() override
    {
        computer_.setRAM("16GB DDR4");
    }

    void buildStorage() override
    {
        computer_.setStorage("1TB SSD");
    }

    Computer getResult() override
    {
        return computer_;
    }*//*
};*/