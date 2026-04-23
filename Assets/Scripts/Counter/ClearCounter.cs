using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if(player.GetKitchenObject()
                .TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                if (IsHaveKitchenObject() == false)
                {
                    TransferKitchenObject(player, this);
                }
                else
                {
                    bool isSuccess = plateKitchenObject.AddKitchenObjectSO( GetKitchenObjectSO());
                    if (isSuccess)
                    {
                        DestroyKitchenObject();
                    }
                }
            }
            else
            {
                if (IsHaveKitchenObject() == false)
                {
                    TransferKitchenObject(player, this);
                }
                else
                {
                    if(GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {

                        if(plateKitchenObject.AddKitchenObjectSO( player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }

                    }

                }
            }

        }
        else
        {
            if (IsHaveKitchenObject() == false)
            {
                
            }
            else
            {
                TransferKitchenObject(this, player);
            }
        }
    }

}