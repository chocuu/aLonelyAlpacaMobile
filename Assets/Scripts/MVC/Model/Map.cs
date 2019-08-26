using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Anonym.Isometric;

/**
 * Model of the map for a specific level with all its blocks.
 */
public class Map {

    Dictionary<Vector2Int, SortedList<int, Block>> map; 
    Block held_block = null;
    Block try_held_block = null;
    static Vector3 hideCoords = new Vector3(-100, -100, -100);

    /**
     * Creates a Map model. 
     * 
     * x_w = width in x axis
     * y_w = width in y axis
     */
    public Map(int x_w, int y_w) {
        map = new Dictionary<Vector2Int, SortedList<int, Block>>();
    }

    /**
     * Adds a block
     * @param {[type]} string          name   [Name of block object (has type)]
     * @param {[type]} Vector3         last   [The previous coords of this block]
     * @param {[type]} Vector3         coords [Coords of this block now]
     * @param {[type]} GridCoordinates obj    [Grid coordinates obj tied to this block (for movement)]
     */
    public void AddBlock(string name, Vector3 last, Vector3 coords, GridCoordinates obj) {
        Block.BlockType bt = Block.BlockType.WALL;
        if (name.Contains("grass") || name.Contains("start")) {
            bt = Block.BlockType.GRASS;
        } else if (name.Contains("magma")) {
            bt = Block.BlockType.LAVA;
        } else if (name.Contains("interactable") || name.Contains("poof")) {
            bt = Block.BlockType.MOVEABLE;
        } else if (name.Contains("winblock")) {
            bt = Block.BlockType.WIN;
        } else if (name.Contains("spinning") || name.Contains("arrow") 
                    || name.Contains("tut") || name.Contains("Bulk")) { //for final && tut level
            bt = Block.BlockType.NONE;
        }// else { // for debugginggz
        //     Debug.Log("wall block name: " + name);
        // }

        Vector2Int xz;

        // Add new block object
        if(bt != Block.BlockType.NONE) {
            coords.x = (float)Math.Round(coords.x);
            coords.y = (float)Math.Round(coords.y);
            coords.z = (float)Math.Round(coords.z);
            Block block;
            block = new Block(bt, coords, obj);
            xz = new Vector2Int((int)coords.x, (int)coords.z);

            SortedList<int, Block> get;
            if(map.TryGetValue(xz, out get)) {
                get.Remove((int)coords.y);
                get.Add((int)coords.y, block);
            } else {
                get = new SortedList<int, Block>();
                map.Add(xz, get);
                get.Add((int)coords.y, block);
            }
            // if(name.Contains("start")) {
                // Debug.Log(name + " " + block.getCoords() + " " + block.b_type);
                // Block get_b;
                // if(get.TryGetValue((int)coords.y, out get_b)) {
                //     Debug.Log("get block in block block: " + get_b.getCoords());
                //     Debug.Log("get block in block block: " + get_b.b_type);
                // }
            // }
        }
    }

    /**
     * Returns block at coordinate. Null of there is no block there.
     * 
     * @param {[type]} Vector3 coords [Desired block coordinates]
     */
    public Block GetBlock(Vector3 coords) {
        coords.x = (float)Math.Round(coords.x);
        coords.y = (float)Math.Round(coords.y);
        coords.z = (float)Math.Round(coords.z);
        Vector2Int xz = new Vector2Int((int)coords.x,(int) coords.z);
        SortedList<int, Block> get;
        if(map.TryGetValue(xz, out get)) {
            Block get_b;
            if(get.TryGetValue((int)coords.y, out get_b)) {
                return get_b;
            }
        }
        return null;
    }

    /**
     * Start animation of poof when trying to hold a block
     */
    public bool LoadTryHoldBlock(Vector3 coords, bool set) {
        if(held_block != null) return false;
        if(set) {
            Vector2Int xz = new Vector2Int((int)coords.x,(int) coords.z);
            SortedList<int, Block> get;
            if(map.TryGetValue(xz, out get)) {
                Block get_b;
                if(get.TryGetValue((int)coords.y, out get_b)) {
                    if(get_b.b_type == Block.BlockType.MOVEABLE) {
                        try_held_block = get_b;
                        get_b.SetAnim(true);
                        return true;
                    }
                }
            }
        } else if(try_held_block != null) {
            try_held_block.SetAnim(false);
        }
        return false;
    }

    /**
     * If a block is already held, try to place a block at coords.
     * (If there is no block at coords, put it at any block below coords.
     * If there is no block below it, then do not drop the block.)
     *
     * If a block is not held, see if there is a moveable block at coords
     * and if so pick it up.
     * 
     * @param {[type]} Vector3 coords [Attempted coordinate]
     */
    public bool TryHoldOrPlaceBlock(Vector3 coords) {
        if(try_held_block != null) {
            try_held_block.SetAnim(false);
        }
        coords.x = (float)Math.Round(coords.x);
        coords.y = (float)Math.Round(coords.y);
        coords.z = (float)Math.Round(coords.z);
        Vector2Int xz = new Vector2Int((int)coords.x,(int) coords.z);
        SortedList<int, Block> get;
        if(held_block == null) { // try hold block
            if(map.TryGetValue(xz, out get)) {
                Block get_b;
                if(get.TryGetValue((int)coords.y, out get_b)) {
                    if(get_b.b_type == Block.BlockType.MOVEABLE) {
                        held_block = get_b;
                        held_block.Move(hideCoords);
                        get.Remove((int)coords.y);
                        return true;
                    }
                }
            }
        } else { // try place block
            if(GetBlock(coords) != null) 
                return false;
            Block top = GetHighestBlockBelow(coords);
            if(top == null)
                return false;
            coords = top.getCoords();
            coords.y++;
            held_block.Unhighlight();
            held_block.setCoords(coords);
            if(map.TryGetValue(xz, out get)) {
                get.Remove((int)coords.y);
                get.Add((int)coords.y, held_block);
            } else {
                get = new SortedList<int, Block>();
                map.Add(xz, get);
                get.Add((int)coords.y, held_block);
            }
            held_block.Move(coords);
            held_block = null;
            return true;
        }
        return false;
    }

    /**
     * Get the highest block that is no higher than coords. If there
     * is a block at coords, return that one. If there isn't, return
     * the highest one below it (null if non).
     * 
     * @param {[type]} Vector3 coords [Attempted coordinate.]
     */
    public Block GetHighestBlockBelow(Vector3 coords) {
        // Debug.Log("Get highest block below " + coords);
        Vector2Int xz = new Vector2Int((int)coords.x,(int) coords.z);
        int y = (int)coords.y;
        SortedList<int, Block> get;
        if(map.TryGetValue(xz, out get)) {
            // Debug.Log("Get highest block below 1" + get.Count);
            Block topBlock = null;
            foreach(KeyValuePair<int, Block> pair in get) {
                // Debug.Log(pair + " " + y);
                if (pair.Key <= y) {
                    // Debug.Log("HERE");
                    topBlock = pair.Value;
                }
            }
            return topBlock;
        }
        return null;
    }

    public bool IsBlockHeld() {
        return held_block != null;
    }

    /**
     * Used to draw moveable block at a lower opacity
     */
    public void PreviewBlock(Vector3 coords) {
        //Debug.Log("preview block start");
        if(held_block == null) return;
        //Debug.Log("1");
        if(GetBlock(coords) != null) 
            return;
        //Debug.Log("2");
        Block top = GetHighestBlockBelow(coords);
        if(top == null)
            return;
        //Debug.Log("3");
        coords = top.getCoords();
        coords.y++;
        coords.x = (float)Math.Round(coords.x);
        coords.y = (float)Math.Round(coords.y);
        coords.z = (float)Math.Round(coords.z);
        held_block.LowOpacity();
        held_block.setCoords(coords);
        // Vector2Int xz = new Vector2Int((int)coords.x,(int) coords.z);
        // SortedList<int, Block> get;
        // if(map.TryGetValue(xz, out get)) {
        //     get.Remove((int)coords.y);
        //     get.Add((int)coords.y, held_block);
        // } else {
        //     get = new SortedList<int, Block>();
        //     map.Add(xz, get);
        //     get.Add((int)coords.y, held_block);
        // }
        held_block.Move(coords);
        //Debug.Log("preview block end");
    }

}
