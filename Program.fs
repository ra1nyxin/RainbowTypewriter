open System
open System.Threading



let hsvToRgb (h: float) (s: float) (v: float) =
    let i = floor (h / 60.0)
    let f = h / 60.0 - i
    let p = v * (1.0 - s)
    let q = v * (1.0 - f * s)
    let t = v * (1.0 - (1.0 - f) * s)

    let (r, g, b) =
        match int i % 6 with
        | 0 -> (v, t, p)
        | 1 -> (q, v, p)
        | 2 -> (p, v, t)
        | 3 -> (p, q, v)
        | 4 -> (t, p, v)
        | _ -> (v, p, q)

    (int (r * 255.0), int (g * 255.0), int (b * 255.0))


let typeWriterRainbow (text: string) (delayMs: int) =
    let len = float text.Length
    for i = 0 to text.Length - 1 do
        let char = text.[i]
        let hue =
            if len <= 1.0 then
                0.0
            else
                (float i / (len - 1.0)) * 270.0

        let (r, g, b) = hsvToRgb hue 1.0 1.0

        printf "\x1b[38;2;%d;%d;%dm%c\x1b[0m" r g b char
        Thread.Sleep(delayMs)
    printfn ""

[<EntryPoint>]
let main argv =
//     let asciiArt = """
// oo                                        
//    oo     OOOOOOOO:       OOOOOOOO!       
//       oOOOO!!!!;;;;O    OO.......:;!O     
//      'OOO!!!;;;;;;;;O  O.......:   ;!O    
//      OOO!!!!;;::::::.OO........:    ;!O   
//      OO!!!!;;:::::..............:   ;!O   
//      OOO!!!;::::::..............:   ;!O   
//       OO!!;;::::::.............:   ;!O    
//        OO!;;::::::......oo.....::::!O     
//          O!!;::::::........oo..:::O       
//            !!!;:::::..........ooO         
//               !!;:::::.......O   oo       
//                 ;;::::.....O        oo  ,o
//                    :::..O              ooo
//                      ::.              oooo
//                       :                   
// """
    let asciiArt = """
`Mb                                                             
  `b                ..rmMMbmy..                                 
   `b            .dMP"'     `"VMb.                              
    `b         .p'              `Mb                             
     `b      ,p'                 `Mb           ,mdMMbm.         
      q.   ,dP                     `b       ,MP"'   `"MMb.      
      `b  .P                        `b    ,P'           `b.     
       `L M                          `L ,P           ***HelloWorld***
        MM                            MM'                       
        `P                            `P                        
     --====--                      --====--            ---======--- 
"""
    typeWriterRainbow asciiArt 10
    0
