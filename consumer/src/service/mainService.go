package service

import (
	"crypto/rand"
	"errors"
	"log"
	"math/big"
)

func ProcessMessage(message []string) error {

	randNum, err := rand.Int(rand.Reader, big.NewInt(10))
	if err != nil {
		return err
	}

	if randNum.Int64()+1 > 3 {
		log.Println("The message processed in the Main Queue:", message)

		return nil
	}

	return errors.New("returned an error")
}

func ProcessDeadMessage(message []string) {

	log.Println("The Dead Message processed in DLQ:", message)

}
