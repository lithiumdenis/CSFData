(deftemplate song
	(slot title (type SYMBOL))
	(slot time (type INTEGER))
)

(deftemplate player
	(slot current (type SYMBOL))
	(slot time (type INTEGER))
	(slot timeStop (type INTEGER))
)

(deftemplate needSong
	(slot title (type SYMBOL))
)

(deffacts simulation
	(player (current null)(time 0)(timeStop 0))
	(song (title title1)(time 30))
	(song (title title2)(time 40))
	(song (title title3)(time 10))
	(song (title title4)(time 20))
	(song (title title5)(time 10))
	(song (title title6)(time 30))
	(song (title title7)(time 40))
	(song (title title8)(time 15))

	(needSong (title title1))
	(needSong (title title12))
	(needSong (title title2))
	(needSong (title title5))
	(needSong (title title11))
	(needSong (title title4))
)

(defrule stepAndPlay
	(declare(salience 10))
	?playerObject <- (player (current ?currentSong)(time ?currentTime)(timeStop ?timeStop))
	?currentSongRef <- (needSong (title ?titleSong))
	(song (title ?titleSong)(time ?timeSong))
	(or (test(eq ?currentSong null)) (test(>= ?currentTime ?timeStop)))
=>
	(modify ?playerObject(current ?titleSong)(time (+ ?currentTime 5))(timeStop (+ ?currentTime ?timeSong)))
	(retract ?currentSongRef)
)

(defrule step
	(declare(salience 10))
	?playerObject <- (player (current ?currentSong)(time ?currentTime)(timeStop ?timeStop))
	(needSong (title ?titleSong))
	(test(< ?currentTime ?timeStop))
=>
	(modify ?playerObject(time (+ ?currentTime 5)))
)


(defrule testSong
	?songRef <-(needSong (title ?curTitle)) 
	=>
		(printout t "sorry, " ?curTitle " not found" crlf)
		(retract ?songRef)
)