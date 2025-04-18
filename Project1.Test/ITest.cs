namespace Project1.Test;
public interface ITest{
    Task TestCreationSuccess();
    Task TestRetrivalSuccess();

    Task TestRetrivalFailureIdNotFound();

    Task TestDeleteSuccess();

    //remember the inheritors can extend this and create their own thing

}