IEnumerator SpawnWaves()
	{
		//wait after game starts
		yield return new WaitForSeconds(startWait);
		while(true)
		{
			adjustWave();
			for (int i=0;i<hazardCount;i++)
			{
				bool hasSpawned = false;
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				if (formation == 1)//line formation
				{
					float xPosTemp  = -spawnValues.x + (i*2.0f);
					float zPosTemp = spawnValues.z  - (Time.deltaTime * alienSpeedMax * i); //fix;
					if (xPosTemp > spawnValues.x)
					{
						xPosTemp = xPosTemp - (spawnValues.x * 2.0f) - 2.0f ;
						zPosTemp = zPosTemp + 3.0f;
					}
					spawnPosition = new Vector3 ( xPosTemp, spawnValues.y, zPosTemp);
				}
				else if (formation == 2)
				{
					float xPosTemp = 0;
					float zPosTemp = spawnValues.z  - (Time.deltaTime * alienSpeedMax * i); //fix;
					if (i != 0)
					{
						if ((i % 2)==0)
						{
							//even
							xPosTemp = xPosTemp + (2.0f*(i/2));
							zPosTemp = zPosTemp + (i-1);
						}
						else
						{
							//odd
							if (i == 1)
							{
								xPosTemp = xPosTemp - 2.0f;
								zPosTemp = zPosTemp + 1.0f;
							}
							else
							{
								xPosTemp = xPosTemp - (2.0f*((i+1)/2));
								zPosTemp = zPosTemp + i;
							}
						}
					}
					spawnPosition = new Vector3 ( xPosTemp, spawnValues.y, zPosTemp);
				}
				Quaternion spawnRotation = Quaternion.identity;

				if (powerUpChance >= Random.Range(1,100))
				{
					spawnRotation.eulerAngles = new Vector3(0, 0, 0);
					dice = Random.Range(1,4); //between 1 and 3
					if (dice == 1)
					{
						var temp = Instantiate(powerUp, spawnPosition, spawnRotation) as GameObject;
						float floatingSpeed = Random.Range (-hazardSpeedMin, -hazardSpeedMin);
						temp.rigidbody.velocity = new Vector3 (0, 0 , floatingSpeed );
					}
					else if (dice == 2)
					{
						var temp = Instantiate(powerUp2, spawnPosition, spawnRotation) as GameObject;
						float floatingSpeed = Random.Range (-hazardSpeedMin, -hazardSpeedMin);
						temp.rigidbody.velocity = new Vector3 (0, 0 , floatingSpeed );
					}
					else if (dice == 3)
					{
						var temp = Instantiate(powerUp3, spawnPosition, spawnRotation) as GameObject;
						float floatingSpeed = Random.Range (-hazardSpeedMin, -hazardSpeedMin);
						temp.rigidbody.velocity = new Vector3 (0, 0 , floatingSpeed );
					}
					hasSpawned = true;
				}

				if ( (alienChance >= Random.Range(1,100))  && (!hasSpawned)) 
				{
					spawnRotation.eulerAngles = new Vector3(0, -180, 0);
					var temp = Instantiate(alien, spawnPosition, spawnRotation) as GameObject;
					temp.GetComponent<DestroyByContant>().health = hazardHealth;
					temp.GetComponent<MoveEnemy>().speed = Random.Range(alienSpeedMin, alienSpeedMax);
					hasSpawned = true;
				}
				if ( (alien2Chance >= Random.Range(1,100)) && (!hasSpawned))
				{
					spawnRotation.eulerAngles = new Vector3(0, -180, 0);
					var temp = Instantiate(alien2, spawnPosition, spawnRotation) as GameObject;
					temp.GetComponent<DestroyByContant>().health = hazardHealth;
					temp.GetComponent<MoveEnemy>().speed = Random.Range(alienSpeedMin, alienSpeedMax);
					hasSpawned = true;
				}
				if ( (alien3Chance >= Random.Range(1,100)) && (!hasSpawned))
				{
					spawnRotation.eulerAngles = new Vector3(0, -180, 0);
					var temp = Instantiate(alien3, spawnPosition, spawnRotation) as GameObject;
					temp.GetComponent<DestroyByContant>().health = hazardHealth;
					temp.GetComponent<MoveEnemy>().speed = Random.Range(alienSpeedMin, alienSpeedMax);
					hasSpawned = true;
				}
				if (!hasSpawned)
				{
					//decice which type of asteriod
					dice = Random.Range(1,4); //between 1 and 3
					if (dice == 1)
					{
						var temp = Instantiate (hazard, spawnPosition, spawnRotation) as GameObject;
						float hazardSize = Random.Range(hazardSizeMin, hazardSizeMax);
						temp.transform.localScale = new Vector3(hazardSize, hazardSize, hazardSize);
						float floatingSpeed = Random.Range (-hazardSpeedMin, -hazardSpeedMin);
						temp.rigidbody.velocity = new Vector3 ((Random.Range(-hazardVarationFactor,hazardVarationFactor)*floatingSpeed), 0 , floatingSpeed );
						temp.GetComponent<DestroyByContant>().health = hazardHealth;
					}
					else if(dice == 2)
					{
						var temp = Instantiate (hazard2, spawnPosition, spawnRotation) as GameObject;
						float hazardSize = Random.Range(hazardSizeMin, hazardSizeMax);
						temp.transform.localScale = new Vector3(hazardSize, hazardSize, hazardSize);
						float floatingSpeed = Random.Range (-hazardSpeedMin, -hazardSpeedMin);
						temp.rigidbody.velocity = new Vector3 ((Random.Range(-hazardVarationFactor,hazardVarationFactor)*floatingSpeed), 0 , floatingSpeed );
						temp.GetComponent<DestroyByContant>().health = hazardHealth;
					}
					else if (dice == 3)
					{
						var temp = Instantiate (hazard3, spawnPosition, spawnRotation) as GameObject;
						float hazardSize = Random.Range(hazardSizeMin, hazardSizeMax);
						temp.transform.localScale = new Vector3(hazardSize, hazardSize, hazardSize);
						float floatingSpeed = Random.Range (-hazardSpeedMin, -hazardSpeedMin);
						temp.rigidbody.velocity = new Vector3 ((Random.Range(-hazardVarationFactor,hazardVarationFactor)*floatingSpeed), 0 , floatingSpeed );
						temp.GetComponent<DestroyByContant>().health = hazardHealth;
					}
				}
				//time between each spawn
				yield return new WaitForSeconds(spawnWait);
			}
			//time between each wave
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				SaveHighScore();
				UpdateHighScore();
				restartText.text = "Press R for Restart";
				restart = true;
				break;
			}
		}
	}
