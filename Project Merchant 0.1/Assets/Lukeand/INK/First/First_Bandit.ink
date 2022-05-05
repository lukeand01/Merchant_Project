INCLUDE Globals.ink


->NARRATION

===NARRATION===
    We must flee...
    peace is no more and the wicked armies of Raven march.
    this broken and divided nation wont hold. 
    and i wont die helping them.
    i- we... must reach the coast and escape this nation before its too late.
    


->SPACE_BETWEEN

===SPACE_BETWEEN===
    [the caravan is moving through a forest. its night and you and your allies hear a noise between the trees.]
    
    

->FIRST



===FIRST===
    Look what we have here, boss.#speaker:Bandit #portrait: bandit_normal #layout:left
    [A Stronger bandit appear between the group. he is clearly the leader]  #speaker: #layout:null
    hum... #speaker:Bandit Leader #portrait:bandit_normal  #layout:right
    You seem eager to run away. 
    have the Raven scared you that much? 
    HAHA! [suddenly one of your companions laugh and stand up from the wagon] #speaker: #layout:null
    look at the petty thief talking. #speaker:warrior
    im sure he thinks he is brave for robbing refugees.
    warrior!#speaker:mage
    dont say those things. they will get angry.
    
    
    
  {PlayerWisdom >= 3:
    *[(Wisdom)You must be a fool to not fear the Raven! one of them could kill all of your friends as he is busy eating you.]
        ->DONE
  }
    *[warrior is right. they are pathetic thiefs and we must laugh at them]
        ->DONE
        
    *[mage is right. We are not looking for a fight]
        ->DONE
        
    *[...]    
        ->DONE
    ->END
   *[(DEBUG MODE) go directly to the city]
        #transition:MapScene
    
        ->DONE
    *[(DEBUG MODE) go directly to the fight]
    
        ->DONE

===AGRESSIVE===
    who said we rob refugees?
    oh no. we are not savages like that.
    instead we rob deserters and we rob merchants. both deserve the humiliation or... if they resist...the blade.
    
    *[thats no problem. because we are refugees.]
        {PlayerSpeech => 2: yo | You}
        ->DONE
    *[i am willing to pay]    
        ->DONE
    *[the blade? please, you will be killed before you can even use it]
    
        ->DONE
    

    ->DONE
    
    
    
    
===PASSIVE===
    nicely said.
    so i will ask
    are you a deserter, refugee or a merchant?
    *[Deserter]
        {PlayerSpeech => 2: yo | yo}
        ->DONE
    *[Refugee]
        {PlayerSpeech => 2: yo| yo}
            ->DONE
    *[Merchant]
        ->MERCHANT
        
->DONE



===MERCHANT===
    very well. you are a merchant.
    the price will be 30 gold. none of that kuhlal bullshit.
    *[Fine]
        ->DONE
        
    *[that much? you have to lower the price]
        {PlayerBarter => 2: yo |You}
        ->DONE
    *[Pay you!? maybe if i am dead!]
        ->DONE
        
->DONE


===REFUGEE===
    
    
->DONE


===DESERTER===
    
->DONE














